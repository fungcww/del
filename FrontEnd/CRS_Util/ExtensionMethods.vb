Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports System.Runtime.CompilerServices


Public Module ExtensionMethods
    Private key As String = "DeadIsNotTheEnd.DeadIsNotTheEnd."

    <Extension()>
    Public Function EncryptString(ByVal plainText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim array As Byte()

        Using aes As Aes = aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv
            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)

            Using memoryStream As MemoryStream = New MemoryStream()

                Using cryptoStream As CryptoStream = New CryptoStream(CType(memoryStream, Stream), encryptor, CryptoStreamMode.Write)

                    Using streamWriter As StreamWriter = New StreamWriter(CType(cryptoStream, Stream))
                        streamWriter.Write(plainText)
                    End Using

                    array = memoryStream.ToArray()
                End Using
            End Using
        End Using

        Return Convert.ToBase64String(array)
    End Function

    <Extension()>
    Public Function DecryptString(ByVal cipherText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim buffer As Byte() = Convert.FromBase64String(cipherText)

        Using aes As Aes = aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv
            Dim decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)

            Using memoryStream As MemoryStream = New MemoryStream(buffer)

                Using cryptoStream As CryptoStream = New CryptoStream(CType(memoryStream, Stream), decryptor, CryptoStreamMode.Read)

                    Using streamReader As StreamReader = New StreamReader(CType(cryptoStream, Stream))
                        Return streamReader.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
    End Function
End Module
