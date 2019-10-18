//Edgar Eduardo Regalado Lopez  18212254
//Programa 25: Arboles binarios, Unidad 4.

using System;
namespace programa_25 {
  class Programa {
    public static void Main(string[] args) {
      ArbolBinarioOrdenado arbol = new ArbolBinarioOrdenado();
      bool continuar_fuera = true;
      bool continuar_dentro = true;


      Console.Title = "Programa 25: Árboles binarios";

      Console.WriteLine("Bienvenido al programa de árboles binarios");
      while(continuar_fuera) {
      	Console.WriteLine("Seleccione una opción: ");
      	Console.WriteLine("1. Inserción");
      	Console.WriteLine("2. Recorridos");
      	Console.WriteLine("3. Altura y Nodos totales");
      	Console.WriteLine("4. Salir");

      	int eleccion = Int32.Parse(Console.ReadLine());

      	switch(eleccion) {
      		case 1:	//inserción
      			while(continuar_dentro) {
      				Console.Write("Introduce el valor a insertar o ('salir') para salir: ");
      				string entrada = Console.ReadLine();
      				if(entrada == "salir") {
      					continuar_dentro = false; //terminar este ciclo interno.
      				} else {
      					arbol.Insertar(entrada);
      				}
      			}
      			break;
      		case 2:	//recorridos e impresión.
      			continuar_dentro = true;
      			while(continuar_dentro) {
      				Console.WriteLine("Seleccione el tipo de recorrido: ");
	      			Console.WriteLine("1. Pre orden");
	      			Console.WriteLine("2. In orden");
	      			Console.WriteLine("3. Post orden");
	      			Console.WriteLine("4. Salir");

	      			int entrada_recorrido = Int32.Parse(Console.ReadLine());
	      			switch(entrada_recorrido) {
	      				case 1:	//Pre orden
	      					arbol.ImprimirPre();
	      					break;
	      				case 2:	//In orden
	      					arbol.ImprimirEntre();
	      					break;
	      				case 3:	//Post orden
	      					arbol.ImprimirPost();
	      					break;
	      				case 4:	//salir
	      					continuar_dentro = false;
	      					break;
	      				default:
	      					Console.WriteLine("Operación introducida inválida");
	      					break;
	      			}
      			}
      			break;
      		case 3:
      			Console.WriteLine("Altura: {0}\tNodos totales: {1}",
      				arbol.RetornarAltura(), arbol.Cantidad());
      			break;
      		case 4:
      			continuar_fuera = false;
      			break;
      		default:
      			Console.WriteLine("Operación introducida inválida.");
      			break;
      		}
      	}

      	Console.WriteLine("Programa finalizado, gracias por usar el programa");
      	Console.ReadLine();
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

    Nodo raiz;
    int cantidad;
    int altura;

    public ArbolBinarioOrdenado() {
      this.raiz = null;
    }

    //------------------------------------INSERCIÓN------------------------------
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

	//------------------------------------PRE ORDEN------------------------------
    //procedimiento para mostrar el arbol en preorden
    private void ImprimirPre(Nodo reco) {
      if(reco != null) {
        Console.Write(reco.info + " ");
        ImprimirPre(reco.izquierda);
        ImprimirPre(reco.derecha);
      }
    }

    public void ImprimirPre() {
    	ImprimirPre(raiz);
    	Console.WriteLine();
    }

    //------------------------------------IN ORDEN------------------------------
    //procedimiento para mostrar el arbol en orden.
    private void ImprimirEntre(Nodo reco) {
      if(reco != null) {
        ImprimirEntre(reco.izquierda);
        Console.Write(reco.info + " ");
        ImprimirEntre(reco.derecha);
      }
    }

    public void ImprimirEntre() {
    	ImprimirEntre(raiz);
    	Console.WriteLine();
    }

	//------------------------------------POST ORDEN------------------------------
    //procedimiento para mostrar el arbol en postorden.
    private void ImprimirPost(Nodo reco) {
      if(reco != null) {
        ImprimirPost(reco.izquierda);
        ImprimirPost(reco.derecha);
        Console.Write(reco.info + " ");
      }
    }

    public void ImprimirPost() {
    	ImprimirPost(raiz);
    	Console.WriteLine();
    }

	//------------------------------------VERIFICACIÓN DE EXISTENCIA------------------------------
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

    //------------------------------------CONTADOR DE NODOS TOTALES------------------------------
    //aumenta el contador de cantidad para contar los nodos
    private void Cantidad(Nodo recorrido) {
    	if(recorrido != null) {
    		cantidad++;
    		Cantidad(recorrido.izquierda);
    		Cantidad(recorrido.derecha);
    	}
    }

    public int Cantidad() {
    	cantidad = 0;
    	Cantidad(raiz);
    	return cantidad;
    }

    //------------------------------------CONTADOR DE ALTURA------------------------------
    private void RetornarAltura(Nodo recorrido, int nivel) {
    	if(recorrido != null) {
    		RetornarAltura(recorrido.izquierda, nivel + 1);
    		if(nivel > altura) {
    			altura = nivel;
    		}
    		RetornarAltura(recorrido.derecha, nivel + 1);
    	}
    }

    public int RetornarAltura() {
    	altura = 0;
    	RetornarAltura(raiz, 1);
    	return altura;
    }


  }
}
