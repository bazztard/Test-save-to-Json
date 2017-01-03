using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class unityCreator : MonoBehaviour {

    string ruta;
    string savejsonDB;

    public GameObject prefab;

    public TextAsset tMNames;     //.txt de nombres masculinos
    public TextAsset tWNames;     //.txt de nombres femeninos
    public TextAsset tLNames;     //.txt de apellidos

    unity unidad;
    public UniDB uDB; //lista de unidades DB 
    
    //algunas variables
    string nameAsigned;
    string lastNameAsigned;
    string nombreCompleto;
    int numIdenti;
    string nombre;
    string sexo;
    string apellido;

    void Start()
    {
        numIdenti = 0; //para sumar al id cada vez que se crea una unidad nueva
        //path del archivo json
        ruta = Application.dataPath + "/Resources/unidad.json";
      /*//dejo el json con el formato para una lista
        savejsonDB = JsonUtility.ToJson(uDB);
        File.WriteAllText(ruta, savejsonDB);*/
        //limpio el json para empezar
        File.WriteAllText(ruta, "");
    }

    public void Creador()
    {
        numIdenti += 1;                     //cada vez q se cree una unidad se sumará un 1 al total

        int caraCruz = Random.Range(0, 2);      //0 hombre, 1 mujer
        int selSex = caraCruz;                 //define cual de los dos sera

        if (selSex == 0)                       //si sale hombre busca en el text nombre de hombre
        {
            createMName();
            createLName();

            nombreCompleto = string.Concat(nameAsigned, " ", lastNameAsigned);

            nombre = nameAsigned;
            apellido = lastNameAsigned;
            sexo = ("Masculino");
        }

        if (selSex == 1)                       //si sale mujer busca en el text nombre de mujer
        {
            createWName();
            createLName();

            nombreCompleto = string.Concat(nameAsigned, " ", lastNameAsigned);

            nombre = nameAsigned;
            apellido = lastNameAsigned;
            sexo = ("Femenino");
        }

        unidad = prefab.GetComponent<unity>(); //tomo el script y asigno valores para luego instanciar
        unidad.id = numIdenti;
        unidad.nombre = nombreCompleto;
        unidad.sexo = sexo;
        
        //instancia
        float randomX = Random.Range(-3, 3);
        float randomZ = Random.Range(-3, 3);
        Vector3 randomSpawn = new Vector3(randomX, 1, randomZ);
        GameObject go = Instantiate(prefab, randomSpawn, Quaternion.identity) as GameObject;
        string goname = ("unidad " + numIdenti + " " + nombreCompleto);
        go.name = goname;

        //Guardar las unidades en una lista //FUNCIONA cada unidad.cs se guarda en la lista
        unidad = go.GetComponent<unity>();
        uDB.ubaseDatos.Add(unidad);


        //AHORA JSON*********************

        //para grabar uno por uno //FUNCIONA pero prefiero list o array //además debo agregar un "\n" para espaciar
        savejsonDB = JsonUtility.ToJson(unidad);
        File.AppendAllText(ruta, savejsonDB.ToString());
        File.AppendAllText(ruta, "\n");

        /*
        //para grabar la list //me pasa la instancia a secas con un valor raro //NO SERIALIZABLE?
        savejsonDB = JsonUtility.ToJson(uDB);
        File.WriteAllText(ruta, savejsonDB.ToString());
        //File.WriteAllText(ruta, savejsonDB);
        */

    }

    void createMName()
    {
            if (tWNames != null)                                  //chequea si hay un archivo de texto
            {
                var number = 0;
                string[] textLines = new string[number];
                textLines = (tMNames.text.Split('\n'));           //luego se le asigna las lineas separadas al array
                var todo = textLines.Length;                      //conteo total todas las lineas
                var random = Random.Range(0, todo);               //se selecciona una de entre todas las lineas
                nameAsigned = textLines[random];                  //se le asigna el string random
            }
        }

        void createWName()
    {
            if (tWNames != null)
            {
                var number = 0;
                string[] textLines = new string[number];
                textLines = (tWNames.text.Split('\n'));
                var todo = textLines.Length;
                var random = Random.Range(0, todo);
                nameAsigned = textLines[random];
            }
        }

        void createLName()
    {
            if (tLNames != null)
            {
                var number = 0;
                string[] textLines = new string[number];
                textLines = (tLNames.text.Split('\n'));
                var todo = textLines.Length;
                var random = Random.Range(0, todo);
                lastNameAsigned = textLines[random];
            }
        }

    }

    [System.Serializable]
    public class UniDB
    {
        public List<unity> ubaseDatos;
    }
     