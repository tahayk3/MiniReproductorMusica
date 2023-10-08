Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window

Public Class Form1
    Dim listacanciones(24) As Object
    Dim objetoLista(24) As Object
    Dim objetoVacio(24) As Object
    Dim c As Integer
    Dim num As Integer = 0
    Dim ruta2 As String = "C:\Datos\"
    Dim archivo As String = "libros.txt"

    Dim cantidadCanciones As Integer = 0
    Dim cantidadCancionesLista As New List(Of String)

    Dim currentIndex As Integer = 0 ' Variable para el índice actual de la canción


    Sub Limpiar()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub

    Sub imprimirdatos()
        Dim fs As FileStream
        Try
            If File.Exists(ruta2) Then
                fs = File.Create(ruta2 & archivo)
                fs.Close()
            Else
                Directory.CreateDirectory(ruta2)
                fs = File.Create(ruta2 & archivo)
                fs.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "No se pudo crear el archivo")
        End Try
        DataGridView1.Rows.Clear()
        DataGridView1.ColumnCount = 7
        With DataGridView1
            .Columns(0).Name = "Titulo"
            .Columns(1).Name = "Duracion"
            .Columns(2).Name = "Artista"
            .Columns(3).Name = "Album"
            .Columns(4).Name = "Anio"
            .Columns(5).Name = "Ruta"
            .Columns(6).Name = "Img"
        End With
        Dim linea As New StreamWriter(ruta2 & archivo)

        For i = 0 To listacanciones.Length - 1 Step 1
            If listacanciones(i) IsNot Nothing Then
                Try
                    DataGridView1.Rows.Add()
                    With DataGridView1.Rows(i)
                        .Cells(0).Value = listacanciones(i).titulo
                        .Cells(1).Value = listacanciones(i).duracion
                        .Cells(2).Value = listacanciones(i).artista
                        .Cells(3).Value = listacanciones(i).Album
                        .Cells(4).Value = listacanciones(i).Anio
                        .Cells(5).Value = listacanciones(i).Ruta
                        .Cells(6).Value = listacanciones(i).Img
                    End With
                    linea.WriteLine(listacanciones(i).titulo & ";" & listacanciones(i).duracion & ";" & listacanciones(i).artista & ";" & listacanciones(i).album & ";" & listacanciones(i).anio & ";" & listacanciones(i).ruta & ";" & listacanciones(i).img)
                Catch ex As Exception

                End Try
            End If
        Next
        linea.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        AxWindowsMediaPlayer1.URL = "C:\ruta\cancion.wav"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        AxWindowsMediaPlayer1.Ctlcontrols.next()
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim titulo As String = TextBox1.Text
        Dim duracion As String = TextBox2.Text
        Dim artista As String = TextBox3.Text
        Dim album As String = TextBox4.Text
        Dim anio As String = MonthCalendar1.Text
        Dim rangoSeleccionado As Date = MonthCalendar1.SelectionStart
        Dim fecha As Integer = rangoSeleccionado.Year
        Dim ruta As String = TextBox5.Text
        Dim img As String = TextBox6.Text


        Dim l As New Cancion()
        l.titulo = TextBox1.Text
        l.duracion = TextBox2.Text
        l.artista = TextBox3.Text
        l.album = TextBox4.Text
        l.anio = fecha
        l.ruta = TextBox5.Text
        l.img = TextBox7.Text

        listacanciones(c) = l
        c += 1
        imprimirdatos()
        Limpiar()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Configurar el OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Archivos de audio|*.mp3;*.wav|Todos los archivos|*.*"
        openFileDialog.Multiselect = True ' Permite seleccionar múltiples archivos

        ' Mostrar el cuadro de diálogo de archivo
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Los archivos seleccionados se almacenan en openFileDialog.FileNames
            For Each archivo As String In openFileDialog.FileNames
                ' Agregar el archivo a una lista o realizar alguna otra acción
                'ListBox1.Items.Add(archivo) ' Agregar a una lista
                TextBox5.Text = archivo
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Verifica si se hizo clic en una fila válida (no en el encabezado)
        If e.RowIndex >= 0 Then
            Dim titulo As String = TextBox6.Text
            ' Obtiene la fila seleccionada
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Dim l As New Cancion()
            l.titulo = selectedRow.Cells(0).Value
            l.duracion = selectedRow.Cells(1).Value
            l.artista = selectedRow.Cells(2).Value
            l.album = selectedRow.Cells(3).Value
            l.anio = CInt(selectedRow.Cells(4).Value)
            l.ruta = selectedRow.Cells(5).Value
            l.img = selectedRow.Cells(6).Value

            objetoLista(num) = l
            crearPlaylist(titulo)
            MessageBox.Show("Cancion agregada a la playlist " + titulo)
            num += 1

        End If
    End Sub

    Sub crearPlaylist(ByVal count As String)
        Dim archivo2 As String = ""
        archivo2 = "playlist_" + count + ".txt"

        Dim fs As FileStream
        Try
            If File.Exists(ruta2) Then
                fs = File.Create(ruta2 & archivo2)
                fs.Close()
            Else
                Directory.CreateDirectory(ruta2)
                fs = File.Create(ruta2 & archivo2)
                fs.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "No se pudo crear el archivo")
        End Try

        DataGridView2.Rows.Clear()
        DataGridView2.ColumnCount = 7
        With DataGridView2
            .Columns(0).Name = "Titulo"
            .Columns(1).Name = "Duracion"
            .Columns(2).Name = "Artista"
            .Columns(3).Name = "Album"
            .Columns(4).Name = "Anio"
            .Columns(5).Name = "Ruta"
            .Columns(6).Name = "Img"
        End With
        Dim linea As New StreamWriter(ruta2 & archivo2)
        For i = 0 To objetoLista.Length - 1 Step 1
            If objetoLista(i) IsNot Nothing Then
                Try
                    DataGridView2.Rows.Add()
                    With DataGridView2.Rows(i)
                        .Cells(0).Value = objetoLista(i).titulo
                        .Cells(1).Value = objetoLista(i).duracion
                        .Cells(2).Value = objetoLista(i).artista
                        .Cells(3).Value = objetoLista(i).Album
                        .Cells(4).Value = objetoLista(i).Anio
                        .Cells(5).Value = objetoLista(i).Ruta
                        .Cells(6).Value = objetoLista(i).Img
                    End With
                    linea.WriteLine(objetoLista(i).titulo & ";" & objetoLista(i).duracion & ";" & objetoLista(i).artista & ";" & objetoLista(i).album & ";" & objetoLista(i).anio & ";" & objetoLista(i).ruta & ";" & objetoLista(i).img)
                Catch ex As Exception

                End Try

            End If
        Next
        linea.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        AxWindowsMediaPlayer1.currentPlaylist.clear()
        currentIndex = 0
        cantidadCancionesLista.Clear()
        DataGridView2.Rows.Clear()
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Archivos de Texto|*.txt"
        openFileDialog.Title = "Seleccionar archivo de lista de reproducción"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Lee el contenido del archivo de texto seleccionado.
            Dim filePath As String = openFileDialog.FileName
            Dim lines As String() = File.ReadAllLines(filePath)

            cantidadCanciones = lines.Length
            ' Limpia la lista de reproducción existente.
            AxWindowsMediaPlayer1.currentPlaylist.clear()

            ' Procesa cada línea del archivo.
            For Each line As String In lines
                ' Divide la línea por el punto y coma.
                Dim parts As String() = line.Split(";"c)

                ' Verifica si hay al menos dos partes en la línea antes de acceder a las dos últimas partes (la ruta de la canción y la imagen).
                If parts.Length >= 2 Then
                    Dim cancion As String = parts(parts.Length - 2)

                    ' Verifica si la última parte es una ruta válida y agrega la canción a la lista de reproducción.
                    If File.Exists(cancion) Then
                        AxWindowsMediaPlayer1.currentPlaylist.appendItem(AxWindowsMediaPlayer1.newMedia(cancion))
                        cantidadCancionesLista.Add(parts(parts.Length - 1))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim titulo As String = TextBox6.Text
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        num = 0
        For i As Integer = 0 To objetoLista.Length - 1
            objetoLista(i) = Nothing
        Next
        DataGridView2.Rows.Clear()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        ' Configurar el OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Archivos de imagenes|*.jpg;.png*|Todos los archivos|*.*"
        openFileDialog.Multiselect = True ' Permite seleccionar múltiples archivos

        ' Mostrar el cuadro de diálogo de archivo
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Los archivos seleccionados se almacenan en openFileDialog.FileNames
            For Each archivo As String In openFileDialog.FileNames
                ' Agregar el archivo a una lista o realizar alguna otra acción
                'ListBox1.Items.Add(archivo) ' Agregar a una lista
                TextBox7.Text = archivo
            Next
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AxWindowsMediaPlayer1.Ctlcontrols.play()
            PictureBox3.Image = Image.FromFile(cantidadCancionesLista(currentIndex))
        Catch ex As Exception
            MessageBox.Show("No hay mas canciones")
        End Try

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            currentIndex += -1
            PictureBox3.Image = Image.FromFile(cantidadCancionesLista(currentIndex))
            AxWindowsMediaPlayer1.Ctlcontrols.previous()
        Catch ex As Exception
            currentIndex += +1
            MessageBox.Show("No hay mas canciones")
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Try
            currentIndex += +1
            PictureBox3.Image = Image.FromFile(cantidadCancionesLista(currentIndex))
            AxWindowsMediaPlayer1.Ctlcontrols.next()
        Catch ex As Exception
            currentIndex += -1
            MessageBox.Show("No hay mas canciones")
        End Try

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
            PictureBox3.Image = Image.FromFile(cantidadCancionesLista(currentIndex))
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            AxWindowsMediaPlayer1.Ctlcontrols.pause()
            PictureBox3.Image = Image.FromFile(cantidadCancionesLista(currentIndex))
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try

    End Sub

End Class
