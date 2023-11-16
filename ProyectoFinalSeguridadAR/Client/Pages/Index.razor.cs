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

        private async Task ActualizarMascara(ChangeEventArgs args)
        {
            if (args is not null)
            {
                string numeroTarjeta = args?.Value?.ToString() ?? string.Empty;
                string numeroTarjetaMascarado = NumeroTarjetaMascarado(numeroTarjeta!);
                if (numeroTarjetaMascarado.Contains('*'))
                    await js.InvokeVoidAsync("actualizarMascara", numeroTarjetaMascarado);
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
            string cadenaHash = CalcularSHA256(Numtarjeta);
            _ = mostrarMensajes.MostrarMensajeExitoso("SHA-256 HASH " + cadenaHash);
            await Task.Delay(4000);
            byte[] key = Cifrado.GenerateRandomKey();
            byte[] iv = Cifrado.GenerateRandomIV();
            byte[] encrypted = Cifrado.EncryptStringToBytes(Numtarjeta, key, iv);
            string cadenafinal = Cifrado.DecryptBytesToString(encrypted, key, iv);
            _ = mostrarMensajes.MostrarMensajeExitoso("Desencriptado :" + cadenafinal);
            await Task.Delay(3000);
            NavManager.NavigateTo("/",true);
        }
        private static string CalcularSHA256(string cadenaoriginal)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(cadenaoriginal));

            // Utiliza la clase BitConverter para convertir los bytes del hash a una cadena hexadecimal
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hash;
        }
    }
}
