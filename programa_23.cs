using System;

class MainClass {
  public static void Main (string[] args) {
    ColaPaises cp = new ColaPaises(4); // cambiar a 50.
    cp.Desplegar();
    cp.Insertar("Mexico");
    cp.Insertar("Argentina");
    cp.Insertar("Guatemala");
    cp.Insertar("Italia");
    cp.Desplegar();
    cp.Insertar("Francia");
    cp.Desplegar();
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
      string valor_retorno = this.paises[this.final];
      this.paises[this.final] = ""; //borra el dato.
      this.final++;
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

  public string CalcularMayorPais() {
    
  }

  public bool Vacia() {
    return this.final == this.frente;
  }

  public bool Llena() {
    return this.frente == this.tamano - 1;
  }
}
