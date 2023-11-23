using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ProyectoFinalSeguridadAR.Client.Models;
using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalSeguridadAR.Client.Pages
{
    public partial class Index
    {
        public Usuario Usuario { get; set; } = new Usuario();
        private string Numtarjeta { get; set; } = string.Empty;
        private string NumtarjetaMascarado { get; set; } = string.Empty;
        private string NumtarjetaDesencriptada { get; set; } = string.Empty;

        private async Task ActualizarMascara(ChangeEventArgs args)
        {
            if (args is not null)
            {
                string numeroTarjeta = args?.Value?.ToString() ?? string.Empty;
                NumtarjetaMascarado = NumeroTarjetaMascarado(numeroTarjeta!);
                if (NumtarjetaMascarado.Contains('*'))
                    await js.InvokeVoidAsync("actualizarMascara", NumtarjetaMascarado);
                Numtarjeta = numeroTarjeta!;
            }
        }
        private static string NumeroTarjetaMascarado(string numeroTarjetaCompleto)
        {
            if (numeroTarjetaCompleto.Length == 16)
            {
                string primeros4 = numeroTarjetaCompleto[..4];
                string ultimos4 = numeroTarjetaCompleto.Substring(12, 4);

                return $"{primeros4}********{ultimos4}";
            }
            else
            {
                return "Número de tarjeta no válido";
            }
        }
        public async Task GuardarAsync()
        {
            //Transforma el numero de tarjeta
            _ = mostrarMensajes.MostrarMensajeExitoso("Cadena de entrada A: " + Numtarjeta);
            Console.WriteLine("Cadena de entrada A: " + Numtarjeta.Trim());
            await Task.Delay(3000);
            _ = mostrarMensajes.MostrarMensajeExitoso("Cadena de entrada Mascara: " + NumtarjetaMascarado);
            Console.WriteLine("Cadena de entrada Mascara: " + NumtarjetaMascarado);
            await Task.Delay(3000);
            string cadenaHash = CalcularSHA256(Numtarjeta.Trim());
            _ = mostrarMensajes.MostrarMensajeExitoso("SHA-256 HASH Entrada AC" + cadenaHash);
            Console.WriteLine("SHA-256 HASH Entrada AC" + cadenaHash);
            await Task.Delay(4000);
            byte[] key = Cifrado.GenerateRandomKey();
            byte[] iv = Cifrado.GenerateRandomIV();
            byte[] encrypted = Cifrado.EncryptStringToBytes(Numtarjeta, key, iv);
            _ = mostrarMensajes.MostrarMensajeExitoso("Encriptado AE " );
            await Task.Delay(4000);
            NumtarjetaDesencriptada = Cifrado.DecryptBytesToString(encrypted, key, iv);
            _ = mostrarMensajes.MostrarMensajeExitoso("Desencriptado B: " + NumtarjetaDesencriptada);
            Console.WriteLine("Desencriptado B: " + NumtarjetaDesencriptada);
            await Task.Delay(4000);
            string cadenaFinalHash = CalcularSHA256(NumtarjetaDesencriptada.Trim());
            _ = mostrarMensajes.MostrarMensajeExitoso("SHA-256 HASH Desencriptado BC" + cadenaFinalHash);
            Console.WriteLine("SHA-256 HASH Desencriptado BC" + cadenaFinalHash);
            await Task.Delay(4000);
            if (StringComparer.OrdinalIgnoreCase.Equals(cadenaHash, cadenaFinalHash))
            {
                _ = mostrarMensajes.MostrarMensajeExitoso("Comparación de HASH AC vs BC fue exitosa");
            }
            else
            {
                _ = mostrarMensajes.MostrarMensajeError("Comparación de HASH AC vs BC fue incorrecta");
            }
            await Task.Delay(4000);
            NavManager.NavigateTo("/", true);
        }
        private static string CalcularSHA256(string cadenaoriginal)
        {
            Console.WriteLine(cadenaoriginal);
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(cadenaoriginal));

            // Utiliza la clase BitConverter para convertir los bytes del hash a una cadena hexadecimal
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hash;
        }
    }
}
