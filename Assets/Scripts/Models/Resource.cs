using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {

    public enum ResourceType { Fuel, Build, Science };

    public enum ResourceSize { Small, Medium, Large };

    ResourceType type;
    ResourceSize size;
}
