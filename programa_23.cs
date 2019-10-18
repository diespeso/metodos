using System;

namespace programa_23 {
  class MainClass {
    public static void Main (string[] args) {
      ColaPaises cp = new ColaPaises(50);
      
      bool continuar = true;

      Console.Title = "Programa 23: Cola de países";
      Console.WriteLine("Bienvenido al programa de colas de países");

      while(continuar) {
        Console.Write("Inserte el nombre de un país (o 'ninguno' para salir): ");
        string entrada = Console.ReadLine();
        if(entrada == "ninguno") {
          continuar = false;
        } else {
          cp.Insertar(entrada);
        }
      }

      cp.Desplegar();

      string mayor_pais = cp.CalcularMayorPais();
      if(mayor_pais == null) {
        Console.WriteLine("No se pudo encontrar el mayor país.");
      } else {
        Console.WriteLine("El país más grande alfabéticamente fue: {0}", mayor_pais);
      }

      Console.WriteLine("Comienza la eliminación");
      continuar = true; //reiniciar la flag.
      while(continuar) {
        Console.Write("Seguir eliminando? s/n: ");
        char decision = Console.ReadLine()[0];
        if(decision == 'n') {
          continuar = false;
        } else {
          string valor_a_eliminar = cp.Eliminar();
          if(valor_a_eliminar != null) { // si fue posible la eliminación, muestra.
            Console.WriteLine("Se eliminó el valor {0}", valor_a_eliminar);
          }
        }

      }


      Console.ReadLine();
    }
}
  class ColaPaises {
    int tamano;
    string[] paises;
    int final; //eliminación
    int frente; //inserción

    public ColaPaises(int tamano) {
      this.tamano = tamano;
      this.paises = new string[tamano];
      this.final = -1;
      this.frente = -1;
    }

    public void Insertar(string pais) {
      if(Llena()) {
        Console.WriteLine("Imposible insertar, cola llena");
      } else {
        this.paises[++this.frente] = pais;
      }

      if(this.final == -1) { //si es la primera inserción, inicializar this.final
        this.final = 0;
      }
    }
   
    //Si regresa null, es porque la cola está vacía y está mal eliminar.
    public string Eliminar() {
      if(Vacia()) {
        Console.WriteLine("Imposible eliminar, cola vacía");

        return null;
      } else {
        string valor_retorno;
        if(this.frente == this.final) { //si está en el mismo sitio
          valor_retorno = this.paises[this.frente]; //guardar el valor a eliminar.
          this.paises[this.final] = ""; //borra el dato
          this.frente = -1; //reiniciar frente y final.
          this.final = -1;
        } else {
          valor_retorno = this.paises[this.final];
          this.paises[this.final] = ""; //borra el dato.
          this.final++;
        }

        return valor_retorno;
      }
    }

    public void Desplegar() {
      if(Vacia()) {
        return; //no tratar de mostrar si está vacía.
      } else {
        for(int i = this.final; i <= this.frente; i++) {
          Console.WriteLine(this.paises[i]);
        }
      }
    }

    //Regresa null si la cola está vacía.
    public string CalcularMayorPais() {
      string resultado;
      if(Vacia()) {
        Console.WriteLine("Imposible encontrar el mayor país en una cola vacía");
        return null;
      } else {
          string[] copia_paises = new string[this.tamano]; 
          this.paises.CopyTo(copia_paises, this.final);         //hacer una copia para no ordenar los valores originales.
          for(int i = this.final; i <= this.frente; i++) {      //bubble sort ascendente, yendo del final al frente
            for(int j = i + 1; j <= this.frente; j++) {
              //la funcion String.Compare regresa un valor mayor a 0 si la primer string es mayor alfabéticamente que la segunda
              if(String.Compare(copia_paises[i], copia_paises[j], StringComparison.Ordinal) > 0) {
                //hacer los movimientos con ayuda de un temporal
                string temp = copia_paises[i]; 
                copia_paises[i] = copia_paises[j];
                copia_paises[j] = temp;
              }
            }
          }
          //el mayor valor quedará al final del array, es decir, al frente de la cola.
          resultado = copia_paises[this.frente];
      }
      return resultado;
    }

    public bool Vacia() {
      return this.final == -1;
    }

    public bool Llena() {
      return this.frente == this.tamano - 1;
    }

  }
}
