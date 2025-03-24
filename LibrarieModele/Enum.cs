using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TipCamera
{
    Single,
    Double,
    Quad,
    Suite,
    Deluxe
}

[Flags]
public enum OptiuniCamera
{
    Niciuna = 0,
    AerConditionat = 1,
    WiFi = 2,
    Frigider = 4,
    TV = 8,
    Balcon = 16
}

