using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace MetodoBiseccion1
{
    class Program
    {
        private static double xu = 0.0;
        private static double xi = 0.0;
        private static double xr = 0.0;
        private static double xr_anterior = 0.0;
        private static double error = 100.0; //error al 100% al iniciar.
        
        private static int cifras = 4; //cifras a considerar al redondeo.

        //bandera, si es true es porque el producto fxi * fxr nos dio 0, termina.
        private static bool terminar_por_cero = false;

        private static Function funcion; //función del usuario.

        static void Main(string[] args)
        {

            Console.Write("Introduzca función: ");
            funcion = new Function("f(x) = " + Console.ReadLine());

            //inicia asignacion
            Console.Write("Introduce el valor para xi: ");
            xi = Double.Parse(Console.ReadLine());
            Console.Write("Introduce el valor para xu: ");
            xu = Double.Parse(Console.ReadLine());
            //termina asignacion
            Console.WriteLine("Criterios: 0 -> iteraciones\t1 -> error");
            Console.Write("Introduce el número de tu criterio de terminación: ");
            int criterio = Int32.Parse(Console.ReadLine());

            if(criterio == 0) //terminación por número de iteraciones
            {

                Console.Write("Introduce el número de iteraciones deseadas: ");
                int iteraciones = Int32.Parse(Console.ReadLine());
                Console.WriteLine("iteración|\t\txi|\txu|\tf(xi)|\txr|\tf(xr)|\tError"); //encabezado

                for (int i = 0; i < iteraciones; i++)
                {
                    xr = CalcularXr(xi, xu);
                    error = CalcularError(xr, xr_anterior);
                    Console.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6::0.0000}%",
                        i + 1, xi, xu, Evaluar(xi), xr, Evaluar(xr), error);
                    xr_anterior = xr; //el xr de ahora será el anterior en la sig. iteración.
                    AjustarAbrazo(Evaluar(xi), Evaluar(xr));
                    if (terminar_por_cero) { break; } //terminar, ya se encontró.
                }

            } else if(criterio == 1) //terminación por tolerancia de error.
            {
                Console.Write("Introduce la tolerancia de error en un número entre 0 y 100: ");
                double error_objetivo = Double.Parse(Console.ReadLine());
 
                Console.WriteLine("iteración|\txi|\txu|\tf(xi)|\txr|\tf(xr)|\tError"); //encabezado
                int i = 0; // solo para contar iteraciones.
                while(error > error_objetivo) //hasta que el error sea menor al objetivo.
                {
                    xr = CalcularXr(xi, xu);
                    Console.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6::0.0000}%",
                        i + 1, xi, xu, Evaluar(xi), xr, Evaluar(xr), CalcularError(xr, xr_anterior));
                    error = CalcularError(xr, xr_anterior);
                    xr_anterior = xr; //el xr de ahora será el anterior en la sig. iteración.
                    AjustarAbrazo(Evaluar(xi), Evaluar(xr));
                    if(terminar_por_cero) { break; } //terminar, ya se encontró.
                    i++; //aumentar contador de iteraciones
                }
            } else
            {
                Console.WriteLine("No es una opción válida, repitiendo...");
                Main(args);
            }

            //mostrar resultados finales
            Console.WriteLine("Raíz: {0:0.0000}, error: {1:0.0000}%", xr, error);

            Console.ReadLine();
        }

        /// <summary>
        /// Evalúa la función dada en x dada.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double Evaluar(double x)
        {
            return Math.Round(funcion.calculate(x), cifras);
        }

        /// <summary>
        /// Devuelve el valor de error de acuerdo a las 2 raíces aproximadas. Es porcentual.
        /// </summary>
        /// <param name="xr_actual"></param>
        /// <param name="xr_anterior"></param>
        /// <returns></returns>
        private static double CalcularError(double xr_actual, double xr_anterior)
        {
            return Math.Round(Math.Abs((xr_actual - xr_anterior) / xr_actual) * 100, cifras);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xu"></param>
        /// <param name="xi"></param>
        /// <returns></returns>
        private static double CalcularXr(double xi, double xu)
        {
            return Math.Round((xi + xu) / 2, cifras);
        }

        /// <summary>
        /// Ajusta las variables, producto negativo -> xu = xr, producto positivo -> xi = xr.
        /// producto nulo -> no hay más que hacer.
        /// </summary>
        /// <param name="fxi"></param>
        /// <param name="fxr"></param>
        private static void AjustarAbrazo(double fxi, double fxr)
        {
            if(fxi * fxr < 0)
            {
                xu = xr;
            } else if (fxi * fxr > 0)
            {
                xi = xr;
            } else
            {
                terminar_por_cero = true;
            }
        }
    }
}
