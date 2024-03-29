﻿using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabGrilla
{
    public partial class formListaUsuarios : Form
    {
        private Usuarios _usuarios;

        public formListaUsuarios()
        {
            InitializeComponent();
            GenerarColumnas();
            this.dgvUsuarios.AutoGenerateColumns = false;
            this.oUsuarios = new Negocio.Usuarios();
            this.dgvUsuarios.DataSource = this.oUsuarios.GetAll();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public Negocio.Usuarios oUsuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }


        private void formListaUsuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'academiaDataSet1.usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter1.Fill(this.academiaDataSet1.usuarios);
            // TODO: This line of code loads data into the 'academiaDataSet.usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter.Fill(this.academiaDataSet.usuarios);

        }

        private void btsnGuardar_Click(object sender, EventArgs e)
        {
            this.GuardarCambios();
            this.RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            this.dgvUsuarios.DataSource = this.oUsuarios.GetAll();
        }

        private void GuardarCambios()
        {
            this.oUsuarios.GuardarCambios((DataTable)this.dgvUsuarios.DataSource);
        }

        private void GenerarColumnas()
        {
            //Creando la columna Nro. Documento
            DataGridViewTextBoxColumn colNroDoc = new DataGridViewTextBoxColumn
            {
                //Creamos la nueva columna y definimos el tipo de columna
                Name = "nro_doc",
                //asignamos un nombre a la columna
                HeaderText = "Nro. Documento",
                //indicamos el título a mostrar
                DataPropertyName = "nro_doc",
                //indicamos con cual columna del DataTable que asignamos al
                //DataSource de la grilla debe vincularse
                DisplayIndex = 0
            };
            // en que posición debe mostrarse, todas las columnas a la derecha
            // de la posición que indiquemos se moverán una posción a la derecha

            this.dgvUsuarios.Columns.Add(colNroDoc);
            //agregamos la columna al DataGridView para que la muestre

            //Creando la columna Tipo Documento
            DataGridViewComboBoxColumn colTipoDoc = new DataGridViewComboBoxColumn
            {
                Name = "tipo_doc",
                HeaderText = "Tipo Documento",
                DataPropertyName = "tipo_doc",
                DisplayIndex = 0,

                /*Agrego items manualmente
                colTipoDoc.Items.Add(1);
                colTipoDoc.Items.Add(2);
                colTipoDoc.Items.Add(3);
                colTipoDoc.Items.Add(4);
                colTipoDoc.Items.Add(5);*/

                DataSource = this.getTiposDocumento(),
                //Asigno la lista de items que son válidos

                ValueMember = "cod_tipo_doc",
                //indico que el valor interno del combo es el
                //valor de la fila elegida y la columna cod_tipo_doc
                //del DataSource que asigné a la columna colTipoDoc

                DisplayMember = "desc_tipo_doc"
            };
            //indico que el valor que se muestra al usuario es el
            //que se corresponde con la columna desc_tipo_doc
            //del DataSource que asigné a colTipoDoc independientemente
            //de la columna de la cual obtiene su valor

            this.dgvUsuarios.Columns.Add(colTipoDoc);

            //Creando la columna Telefono
            DataGridViewTextBoxColumn colTel = new DataGridViewTextBoxColumn
            {
                Name = "telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "telefono"
            };

            //Creando la columna Email
            DataGridViewTextBoxColumn colEmail = new DataGridViewTextBoxColumn
            {
                Name = "email",
                HeaderText = "Email",
                DataPropertyName = "email"
            };

            //Creando la columna Celular
            DataGridViewTextBoxColumn colCel = new DataGridViewTextBoxColumn
            {
                Name = "celular",
                HeaderText = "Celular",
                DataPropertyName = "celular"
            };

            //Creando la columna Usuario
            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn
            {
                Name = "usuario",
                HeaderText = "Usuario",
                DataPropertyName = "usuario"
            };

            //Creando la columna Clave
            DataGridViewTextBoxColumn colClave = new DataGridViewTextBoxColumn
            {
                Name = "clave",
                HeaderText = "Clave",
                DataPropertyName = "clave"
            };

            colEmail.Width = 250;
            colNroDoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colClave.Visible = false;

            //como las columnas direccion, nombre, apellido y fecha de nacimiento las creamos
            //con el diseñador de formularios no disponemos de una variable para hacer 
            //referencia a ellas. Entonces debemos referenciarlas con 
            //this.dgvUsuarios.Columns["nombre_columna"] donde el nombre_columna es lo que 
            //indicamos en la propiedad Name de las columnas

            this.dgvUsuarios.Columns["direccion"].Width = 250;
            this.dgvUsuarios.Columns["apellido"].DefaultCellStyle.Font = new Font(this.dgvUsuarios.DefaultCellStyle.Font, FontStyle.Bold);
            this.dgvUsuarios.Columns["nombre"].DefaultCellStyle.Font = new Font(this.dgvUsuarios.DefaultCellStyle.Font, FontStyle.Bold);
            this.dgvUsuarios.Columns["fecha_nac"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvUsuarios.Columns.Add(colTel);
            this.dgvUsuarios.Columns.Add(colEmail);
            this.dgvUsuarios.Columns.Add(colCel);
            this.dgvUsuarios.Columns.Add(colUsuario);
            this.dgvUsuarios.Columns.Add(colClave);

        }

        private DataTable getTiposDocumento()
        {
            //Creo DataTable
            DataTable dtTiposDoc = new DataTable();

            //Agrego columnas al DataTable
            dtTiposDoc.Columns.Add("cod_tipo_doc", typeof(int));
            dtTiposDoc.Columns.Add("desc_tipo_doc", typeof(string));

            //Agrego filas al DataTable
            dtTiposDoc.Rows.Add(new object[] { 1, "DNI" });
            dtTiposDoc.Rows.Add(new object[] { 2, "Cédula" });
            dtTiposDoc.Rows.Add(new object[] { 3, "Pasaporte" });
            dtTiposDoc.Rows.Add(new object[] { 4, "Libreta Cívica" });
            dtTiposDoc.Rows.Add(new object[] { 5, "Libreta Enrolamiento" });

            return dtTiposDoc;

        }
    }
}