using System.Collections.Generic;
using UnityEngine;

public class MyFunction
{
    private List<float> variables;

    public int Count => variables.Count;

    public MyFunction(List<float> list)
    {
        variables = new List<float>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            variables[i] = list[i];
        }
    }

    public float this[float x]
    {
        get
        {
            float result = 0;
            for (int i = 0; i < variables.Count; i++)
            {
                result += x * Mathf.Pow(variables[i], i);
            }

            return result;
        }
    }

    public static MyFunction operator -(MyFunction f1, MyFunction f2)
    {
        MyFunction higher, lower;

        if (f1.Count >= f2.Count)
        {
            higher = f1;
            lower = f2;
        }
        else
        {
            higher = f2;
            lower = f1;
        }

        List<float> vars = new List<float>();
        int i;
        for (i = 0; i < lower.Count; i++)
        {
            float var = higher[i] - lower[i];
            vars.Add(var);
        }

        for (; i < higher.Count; i++)
        {
            float var = higher[i];
            vars.Add(var);
        }

        if (higher == f2)
        {
            for (i = 0; i < higher.Count; i++)
            {
                vars[i] *= -1;
            }
        }

        return new MyFunction(vars);
    }

    public static MyFunction operator +(MyFunction f1, MyFunction f2)
    {
        MyFunction higher, lower;

        if (f1.Count >= f2.Count)
        {
            higher = f1;
            lower = f2;
        }
        else
        {
            higher = f2;
            lower = f1;
        }

        List<float> vars = new List<float>();
        int i;
        for (i = 0; i < lower.Count; i++)
        {
            float var = higher[i] + lower[i];
            vars.Add(var);
        }

        for (; i < higher.Count; i++)
        {
            float var = higher[i];
            vars.Add(var);
        }

        return new MyFunction(vars);
    }

    public MyFunction Integral
    {
        get
        {
            List<float> result = new();
            result.Add(0f);

            for (int i = 0; i < variables.Count; i++)
            {
                float var = variables[i] / (i + 1);
                result.Add(var);
            }
            
            return new MyFunction(result);
        }
    }

    public MyFunction Differential
    {
        get
        {
            List<float> result = new();

            for (int i = 1; i < variables.Count; i++)
            {
                float var = variables[i] * i;
                result.Add(var);
            }

            return new MyFunction(result);
        }
    }
}