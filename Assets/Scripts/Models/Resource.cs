using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {

    public enum ResourceKind { Fuel, Build, Science };
    public enum ResourceSize { Small, Medium, Large };

    public ResourceKind Kind { get; protected set; }
    public ResourceSize Size { get; protected set; }

    public float Life { get; }

    public Resource(ResourceKind kind, ResourceSize size) {
        this.Kind = kind;
        this.Size = size;

        switch (size) {
            case ResourceSize.Small:
                this.Life = 50.0f;
                break;
            case ResourceSize.Medium:
                this.Life = 80.0f;
                break;
            case ResourceSize.Large:
                this.Life = 120.0f;
                break;
            default:
                Debug.LogError("Incompatible size on Resource init");
                break;
        }
    }
}
