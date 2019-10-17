//Edgar Eduardo Regalado Lopez  18212254
//Programa 25: Arboles binarios, Unidad 4.

using System;
namespace programa_25 {
  class Programa {
    public static void Main(string[] args) {
      ArbolBinarioOrdenado arbol = new ArbolBinarioOrdenado();
      arbol.Insertar("Juan");
      arbol.Insertar("Pedro");
      arbol.Insertar("Maria");
      arbol.Insertar("Edgar");
      arbol.Insertar("Alejandro");
      arbol.ImprimirTest();
    }
  }

  class ArbolBinarioOrdenado {
    class Nodo {
      public string info;
      public Nodo izquierda;
      public Nodo derecha;

      public Nodo(string info) {
        this.info = info;
        this.izquierda= null;
        this.derecha = null;
      }

      public Nodo() {
        this.info = "";
        this.izquierda = null;
        this.derecha = null;
      }
    }

    //FUNCIÓN DE PRUEBA, QUITAR
    public void ImprimirTest() {
      ImprimirPost(this.raiz);
    }

    Nodo raiz;
    int cantidad;
    int altura;

    public ArbolBinarioOrdenado() {
      this.raiz = null;
    }

    public void Insertar(string info) {
      if(!Existe(info)) {
        Nodo nuevo = new Nodo(info);
        if(raiz == null) { //si esta vacio establecer la raiz.
          raiz = nuevo;
        } else {
          Nodo anterior = null;
          Nodo recorrido = raiz;
          while(recorrido != null) { //mientras no termine el recorrido
            anterior = recorrido;
            int comparacion = String.Compare(info, recorrido.info,
            StringComparison.Ordinal);
            if(comparacion < 0){ // info es menor a la del nodo actual
              recorrido = recorrido.izquierda;
            } else {
              recorrido = recorrido.derecha;
            }
          }
          int comparacion_ = String.Compare(info, anterior.info,
          StringComparison.Ordinal);
          if(comparacion_ < 0) { //info es menor a anterior.info
            anterior.izquierda = nuevo;
          } else {
            anterior.derecha = nuevo;
          }
        }
      }
    }

    //procedimiento para mostrar el arbol en preorden
    private void ImprimirPre(Nodo reco) {
      if(reco != null) {
        Console.Write(reco.info + " ");
        ImprimirPre(reco.izquierda);
        ImprimirPre(reco.derecha);
      }
    }

    //procedimiento para mostrar el arbol en orden.
    private void ImprimirEntre(Nodo reco) {
      if(reco != null) {
        ImprimirEntre(reco.izquierda);
        Console.Write(reco.info + " ");
        ImprimirEntre(reco.derecha);
      }
    }

    //procedimiento para mostrar el arbol en postorden.
    private void ImprimirPost(Nodo reco) {
      if(reco != null) {
        ImprimirPost(reco.izquierda);
        ImprimirPost(reco.derecha);
        Console.Write(reco.info + " ");
      }
    }

    //Función que verifica si un valor ya existe en el arbol o no.
    public bool Existe(string info) {
      Nodo recorrido = this.raiz;
      while(recorrido != null) {
        //usar stringComparison, da valores negativos si el segundo
        //argumento va después del primero, positivos si el primer
        //argumento va después del segundo, y 0 si son iguales.
        int resultadoComparacion = String.Compare(info, recorrido.info, StringComparison.Ordinal);
        if(resultadoComparacion == 0) {
          return true;
        } else if(resultadoComparacion > 0) {
          //si es mayor, tomar al lado derecho, sino el izquierdo.
          recorrido = recorrido.derecha;
        } else {
          recorrido = recorrido.izquierda;
        }
      }
      return false; //si no regresó true fue porque no se encontró
    }


  }
}
