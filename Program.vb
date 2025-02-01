Imports System.IO

Module Module1
    Sub Main()
        Dim opcion As Char
        Dim opcion2 As ConsoleKeyInfo
        Dim nombre, imc2 As String
        Dim peso, altura, imc As Double

        Do
            Try
                Console.Clear()
                Console.WriteLine("Menú de opciones:")
                Console.WriteLine("1. Calcular IMC")
                Console.WriteLine("2. Ver historial")
                Console.WriteLine("3. Borrar historial")
                Console.WriteLine("4. Salir")
                Console.Write("Seleccione una opción: ")
                opcion2 = Console.ReadKey()
                opcion = opcion2.KeyChar
                Console.Clear()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Select Case opcion
                Case "1"
                    Try
                        Console.Clear()
                        Console.Write("Ingrese su nombre: ")
                        nombre = Console.ReadLine()
                        Console.Write("Ingrese su peso (kg): ")
                        peso = Double.Parse(Console.ReadLine())
                        Console.Write("Ingrese su altura (m): ")
                        altura = Double.Parse(Console.ReadLine())
                        Console.Clear()

                        If altura = 0 Then
                            Throw New DivideByZeroException("La altura no puede ser cero.")
                        End If

                        imc = peso / (altura * altura)
                        Dim resultado As String = If(imc <= 18.5, "Bajo de peso",
                            If(imc <= 24.9, "Peso normal",
                            If(imc <= 29.9, "Sobrepeso",
                            If(imc <= 34.9, "Obesidad grado 1",
                            If(imc <= 39.9, "Obesidad grado 2", "Obesidad grado 3 (mórbida)")))))

                        imc2 = imc.ToString("F4")
                        Console.WriteLine("Su IMC es: " & imc2 & " - " & resultado)

                        Using historial As StreamWriter = File.AppendText("historial_imc.txt")
                            historial.WriteLine(nombre & ", " & peso & ", " & altura & ", " & imc2 & ", " & resultado)
                        End Using

                        Console.WriteLine("Resultado guardado en el historial.")
                    Catch ex As FormatException
                        Console.WriteLine("Error: Ingrese valores num�ricos válidos.")
                    Catch ex As DivideByZeroException
                        Console.WriteLine(ex.Message)
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    Console.ReadKey()

                Case "2"
                    Console.Clear()
                    Try
                        If Not File.Exists("historial_imc.txt") Then
                            Throw New FileNotFoundException("El archivo de historial no existe.")
                        End If

                        Using historial As StreamReader = File.OpenText("historial_imc.txt")
                            Console.WriteLine("Nombre, Peso, Altura, IMC, Resultado")
                            Do Until historial.EndOfStream
                                Console.WriteLine(historial.ReadLine())
                            Loop
                        End Using
                    Catch ex As FileNotFoundException
                        Console.WriteLine(ex.Message)
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    Console.WriteLine("\nPresione cualquier tecla para continuar...")
                    Console.ReadKey()

                Case "3"
                    Try
                        If File.Exists("historial_imc.txt") Then
                            File.Delete("historial_imc.txt")
                            Console.WriteLine("Historial eliminado exitosamente.")
                        Else
                            Console.WriteLine("No hay historial para borrar.")
                        End If
                    Catch ex As Exception
                        Console.WriteLine("Error al borrar el historial: " & ex.Message)
                    End Try
                    Console.ReadKey()

                Case "4"
                    Console.Clear()
                    Console.WriteLine("Hasta pronto.")
                    Exit Do

                Case Else
                    Console.WriteLine("Opción no válida.")
                    Console.ReadKey()
            End Select

        Loop
    End Sub
End Module
