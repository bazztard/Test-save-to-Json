using UnityEngine;
using System.Collections;

[System.Serializable]
public class unity : MonoBehaviour
{
    public int id;
    public string nombre;
    public string sexo;
        
    public unity(int Id, string Name, string Sex)
    {
        this.id = Id;
        this.nombre = Name;
        this.sexo = Sex;
    }    
}
