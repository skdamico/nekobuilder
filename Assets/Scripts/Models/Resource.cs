using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {

    public enum ResourceKind { Fuel, Build, Science };
    public enum ResourceSize { Small, Medium, Large };

    ResourceKind _kind;
    public ResourceKind Kind { get; protected set; }

    ResourceSize _size;
    public ResourceSize Size { get; protected set; }

    float _life;
    public float Life { get; }

    public Resource(ResourceKind kind, ResourceSize size) {
        this.Kind = kind;
        this.Size = size;

        switch (size) {
            case ResourceSize.Small:
                this.Life = 50.0f;
                break;
            case ResourceSize.Medium:
                this.Life = 200.0f;
                break;
            case ResourceSize.Large:
                this.Life = 500.0f;
                break;
            default:
                Debug.LogError("Incompatible size on Resource init");
                break;
        }
    }
}
