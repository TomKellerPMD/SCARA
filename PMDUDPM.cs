//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.

// PMDUDPM.cs
// User Defined Profile Mode (UDPM) C# module.
// Allocates the UDPM object and the associated subsequent BUFFER objects. 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDLibrary;


public class UDPM
{
    public int m_StartIndex=0;
    public PMDSource m_Source = PMDSource.Source_Time;
    public PMD.PMDAxisNumber m_SourceAxis = PMD.PMDAxisNumber.Axis1;
    public float m_RateScalar = 1.0F;
    public int m_StartValue = 0;
    public int m_StopValue = 2000000000;
    public uint num_vectors;
    private static bool first_object = true;
    private static short next_buffer = 5;
    private static BUFFER sharedI;
    BUFFER Ipoints;
    BUFFER Ppoints;
    PMD.PMDAxis myaxis;
    double temp;
   // int tScalar;
    int tSource;
     
    // The UDPM object.  Each axis has its own UDPM object also referred as the axis's "path".
    public UDPM(PMD.PMDDevice dev,PMD.PMDAxis pAxis,bool shareIBuffer, uint length)
    { 
        myaxis = pAxis;
        if (!shareIBuffer || first_object)
        {
            Ipoints = new BUFFER(dev, pAxis, next_buffer++, length);
            if (shareIBuffer)
            {
                sharedI = Ipoints;
                first_object = false;
            }
        }
        else
        { 
            Ipoints = sharedI;
        }
        Ppoints = new BUFFER(dev, pAxis,next_buffer++ , length);
        Init();
    }

    public void Init()
    {
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.StartIndex, m_StartIndex);
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.PBufferID, Ppoints.bufferID);
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.IBufferID, Ipoints.bufferID);
        tSource = (UInt16)(((ushort)m_Source) << 8);
        tSource += (UInt16)m_SourceAxis;
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.Source, (int)tSource);
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.StartValue, m_StartValue);
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.StopValue, m_StopValue);
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, 0);
    }

    public void Write(Int32[] Idata, Int32[] Pdata, uint length)
    {
        SetPathLength(length); 
        Ipoints.Write(Idata, length);
        Ppoints.Write(Pdata, length);
        
    }
    
    // This write is used when X and Y share the Idata buffer. 
    public void Write(Int32[] Pdata, uint length)
    {
        SetPathLength(length);
        Ppoints.Write(Pdata, length);
    }

    public void Read(Int32[] Idata, Int32[] Pdata)
    {
        Ipoints.Read(ref Idata, num_vectors);
        Ppoints.Read(ref Pdata, num_vectors);
    }

    public void Source(PMDSource source)
    {
        Int32 tSource;
        m_Source = source;
        tSource = (UInt16)(((ushort)source) << 8);
        tSource += (UInt16)m_SourceAxis;
        myaxis.SetProfileParameter(PMD.PMDProfileParameter.Source, tSource);
    }

    public void SetPathLength(uint length)
    {
        myaxis.SetBufferLength(Ipoints.bufferID, (int)length);
        myaxis.SetBufferLength(Ppoints.bufferID, (int)length);
        num_vectors = length;
    }
    public enum PMDSource
    {
            Source_Commanded=0,
            Source_Actual=1,
            Source_Time=2
    }
   
    public enum PMDUDPMTraceVariable{
        PMDTraceDataStreamValue = 90,
        PMDTraceDataStreamIndex = 91,
        PMDTraceContourOutput = 92,
        PMDTraceContourOffset = 93,
        PMDTraceActiveRateScalar = 94
    }
    
    enum PMDRuntimeError{
        PMDErrorIBuffer = 1,
        PMDErrorOverrun = 2,
        PMDErrorPBuffer = 3,
        PMDErrorInvalidIndex = 4
    }
    
}


