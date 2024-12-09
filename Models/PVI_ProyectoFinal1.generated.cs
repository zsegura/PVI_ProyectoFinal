//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : PVI_ProyectoFinal
	/// Data Source    : IN3114
	/// Server Version : 16.00.1135
	/// </summary>
	public partial class PviProyectoFinalDB : LinqToDB.Data.DataConnection
	{
		public ITable<Bitacora>     Bitacoras      { get { return this.GetTable<Bitacora>(); } }
		public ITable<Casa>         Casas          { get { return this.GetTable<Casa>(); } }
		public ITable<Categoria>    Categorias     { get { return this.GetTable<Categoria>(); } }
		public ITable<Cobro>        Cobros         { get { return this.GetTable<Cobro>(); } }
		public ITable<DetalleCobro> DetalleCobroes { get { return this.GetTable<DetalleCobro>(); } }
		public ITable<Persona>      Personas       { get { return this.GetTable<Persona>(); } }
		public ITable<Servicio>     Servicios      { get { return this.GetTable<Servicio>(); } }

		public PviProyectoFinalDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PviProyectoFinalDB(DataOptions<PviProyectoFinalDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Bitacora")]
	public partial class Bitacora
	{
		[Column("id_bitacora"), PrimaryKey, Identity] public int      IdBitacora { get; set; } // int
		[Column("detalle"),     NotNull             ] public string   Detalle    { get; set; } // varchar(255)
		[Column("id_user"),     NotNull             ] public int      IdUser     { get; set; } // int
		[Column("fecha"),       NotNull             ] public DateTime Fecha      { get; set; } // datetime
		[Column("id_cobro"),    NotNull             ] public int      IdCobro    { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Cobros_Bitacora_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdBitacora", OtherKey="IdBitacora", CanBeNull=true)]
		public IEnumerable<Cobro> Cobros { get; set; }

		/// <summary>
		/// FK_Bitacora_id_cobro (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=false)]
		public Cobro Idcobro { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__01142BA1 (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdUser", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Iduse01142BA { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__46E78A0C (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdUser", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Iduse46E78A0C { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__47DBAE45 (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdUser", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Iduse47DBAE { get; set; }

		/// <summary>
		/// FK_Bitacora_id_user (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdUser", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Iduser { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Casas")]
	public partial class Casa
	{
		[Column("id_casa"),             PrimaryKey, Identity] public int      IdCasa             { get; set; } // int
		[Column("nombre_casa"),         NotNull             ] public string   NombreCasa         { get; set; } // nvarchar(255)
		[Column("metros_cuadrados"),    NotNull             ] public int      MetrosCuadrados    { get; set; } // int
		[Column("numero_habitaciones"), NotNull             ] public int      NumeroHabitaciones { get; set; } // int
		[Column("numero_banos"),        NotNull             ] public int      NumeroBanos        { get; set; } // int
		[Column("precio"),              NotNull             ] public decimal  Precio             { get; set; } // decimal(15, 2)
		[Column("id_persona"),          NotNull             ] public int      IdPersona          { get; set; } // int
		[Column("fecha_construccion"),  NotNull             ] public DateTime FechaConstruccion  { get; set; } // date
		[Column("estado"),              NotNull             ] public bool     Estado             { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK__Cobros__id_casa__4BAC3F29_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public IEnumerable<Cobro> Cobrosidcasa4BAC3F { get; set; }

		/// <summary>
		/// FK__Cobros__id_casa__4CA06362_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public IEnumerable<Cobro> Cobrosidcasa4Cas { get; set; }

		/// <summary>
		/// FK_Cobros_id_casa_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public IEnumerable<Cobro> Cobrosidcasas { get; set; }

		/// <summary>
		/// FK__Casas__id_person__49C3F6B7 (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Idperson49C3F6B { get; set; }

		/// <summary>
		/// FK__Casas__id_person__4AB81AF0 (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Idperson4AB81AF { get; set; }

		/// <summary>
		/// FK_Casas_id_persona (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Idpersona { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Categorias")]
	public partial class Categoria
	{
		[Column("id_categoria"), PrimaryKey, Identity] public int    IdCategoria { get; set; } // int
		[Column("nombre"),       Nullable            ] public string Nombre      { get; set; } // varchar(100)
		[Column("descripcion"),  Nullable            ] public string Descripcion { get; set; } // text
		[Column("estado"),       Nullable            ] public bool?  Estado      { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK__Servicios__id_ca__5165187F_BackReference (dbo.Servicios)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=true)]
		public IEnumerable<Servicio> Serviciosidca5165187F { get; set; }

		/// <summary>
		/// FK_Servicios_id_categoria_BackReference (dbo.Servicios)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=true)]
		public IEnumerable<Servicio> Serviciosidcategorias { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Cobros")]
	public partial class Cobro
	{
		[Column("id_cobro"),     PrimaryKey, Identity] public int       IdCobro     { get; set; } // int
		[Column("id_casa"),      Nullable            ] public int?      IdCasa      { get; set; } // int
		[Column("mes"),          Nullable            ] public int?      Mes         { get; set; } // int
		[Column("anno"),         Nullable            ] public int?      Anno        { get; set; } // int
		[Column("id_bitacora"),  Nullable            ] public int?      IdBitacora  { get; set; } // int
		[Column("estado"),       Nullable            ] public string    Estado      { get; set; } // varchar(50)
		[Column("monto"),        Nullable            ] public decimal?  Monto       { get; set; } // decimal(15, 2)
		[Column("fecha_pagada"), Nullable            ] public DateTime? FechaPagada { get; set; } // date
		[Column("id_persona"),   Nullable            ] public int?      IdPersona   { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Cobros_Bitacora (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdBitacora", OtherKey="IdBitacora", CanBeNull=true)]
		public Bitacora Bitacora { get; set; }

		/// <summary>
		/// FK_Bitacora_id_cobro_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraidcobroes { get; set; }

		/// <summary>
		/// FK_DetalleCobro_id_cobro_BackReference (dbo.DetalleCobro)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=true)]
		public IEnumerable<DetalleCobro> DetalleCobroidcobroes { get; set; }

		/// <summary>
		/// FK_Cobros_id_casa (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public Casa Idcasa { get; set; }

		/// <summary>
		/// FK__Cobros__id_casa__4BAC3F29 (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public Casa Idcasa4BAC3F { get; set; }

		/// <summary>
		/// FK__Cobros__id_casa__4CA06362 (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdCasa", OtherKey="IdCasa", CanBeNull=true)]
		public Casa Idcasa4CA { get; set; }

		/// <summary>
		/// FK_Cobros_Persona (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public Persona Persona { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="DetalleCobro")]
	public partial class DetalleCobro
	{
		[Column("id_servicio"), PrimaryKey(1), NotNull] public int IdServicio { get; set; } // int
		[Column("id_cobro"),    PrimaryKey(2), NotNull] public int IdCobro    { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_DetalleCobro_id_cobro (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdCobro", OtherKey="IdCobro", CanBeNull=false)]
		public Cobro Idcobro { get; set; }

		/// <summary>
		/// FK_DetalleCobro_id_servicio (dbo.Servicios)
		/// </summary>
		[Association(ThisKey="IdServicio", OtherKey="IdServicio", CanBeNull=false)]
		public Servicio Idservicio { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Persona")]
	public partial class Persona
	{
		[Column("id_persona"),       PrimaryKey,  Identity] public int       IdPersona       { get; set; } // int
		[Column("nombre"),           NotNull              ] public string    Nombre          { get; set; } // varchar(100)
		[Column("apellido"),         NotNull              ] public string    Apellido        { get; set; } // varchar(100)
		[Column("email"),            NotNull              ] public string    Email           { get; set; } // varchar(150)
		[Column("telefono"),            Nullable          ] public string    Telefono        { get; set; } // varchar(15)
		[Column("direccion"),           Nullable          ] public string    Direccion       { get; set; } // varchar(255)
		[Column("fecha_nacimiento"),    Nullable          ] public DateTime? FechaNacimiento { get; set; } // date
		[Column("contrasena"),       NotNull              ] public string    Contrasena      { get; set; } // varchar(255)
		[Column("estado"),              Nullable          ] public bool?     Estado          { get; set; } // bit
		[Column("tipo_persona"),        Nullable          ] public string    TipoPersona     { get; set; } // varchar(50)

		#region Associations

		/// <summary>
		/// FK__Bitacora__id_use__01142BA1_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdUser", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraiduse01142Bas { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__46E78A0C_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdUser", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraiduse46E78A0C { get; set; }

		/// <summary>
		/// FK__Bitacora__id_use__47DBAE45_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdUser", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraiduse47Dbaes { get; set; }

		/// <summary>
		/// FK_Bitacora_id_user_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdUser", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoraidusers { get; set; }

		/// <summary>
		/// FK__Casas__id_person__49C3F6B7_BackReference (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Casa> Casasidperson49C3F6B { get; set; }

		/// <summary>
		/// FK__Casas__id_person__4AB81AF0_BackReference (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Casa> Casasidperson4AB81Afs { get; set; }

		/// <summary>
		/// FK_Casas_id_persona_BackReference (dbo.Casas)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Casa> Casasidpersonas { get; set; }

		/// <summary>
		/// FK_Cobros_Persona_BackReference (dbo.Cobros)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Cobro> Cobros { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Servicios")]
	public partial class Servicio
	{
		[Column("id_servicio"),  PrimaryKey,  Identity] public int      IdServicio  { get; set; } // int
		[Column("nombre"),          Nullable          ] public string   Nombre      { get; set; } // varchar(100)
		[Column("descripcion"),     Nullable          ] public string   Descripcion { get; set; } // text
		[Column("precio"),          Nullable          ] public decimal? Precio      { get; set; } // decimal(10, 2)
		[Column("id_categoria"), NotNull              ] public int      IdCategoria { get; set; } // int
		[Column("estado"),          Nullable          ] public bool?    Estado      { get; set; } // bit

		#region Associations

		/// <summary>
		/// FK_DetalleCobro_id_servicio_BackReference (dbo.DetalleCobro)
		/// </summary>
		[Association(ThisKey="IdServicio", OtherKey="IdServicio", CanBeNull=true)]
		public IEnumerable<DetalleCobro> DetalleCobroidservicios { get; set; }

		/// <summary>
		/// FK__Servicios__id_ca__5165187F (dbo.Categorias)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=false)]
		public Categoria Idca5165187F { get; set; }

		/// <summary>
		/// FK_Servicios_id_categoria (dbo.Categorias)
		/// </summary>
		[Association(ThisKey="IdCategoria", OtherKey="IdCategoria", CanBeNull=false)]
		public Categoria Idcategoria { get; set; }

		#endregion
	}

	public static partial class PviProyectoFinalDBStoredProcedures
	{
		#region SpActualizarCobro

		public static int SpActualizarCobro(this PviProyectoFinalDB dataConnection, int? @IdCobro, string @NuevosServicios)
		{
			var parameters = new []
			{
				new DataParameter("@IdCobro",         @IdCobro,         LinqToDB.DataType.Int32),
				new DataParameter("@NuevosServicios", @NuevosServicios, LinqToDB.DataType.VarChar)
				{
					Size = -1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[spActualizarCobro]", parameters);
		}

		#endregion

		#region SpAlterdiagram

		public static int SpAlterdiagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, int? @version, byte[] @definition)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary)
				{
					Size = -1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_alterdiagram]", parameters);
		}

		#endregion

		#region SpAuthenticateUser

		public static int SpAuthenticateUser(this PviProyectoFinalDB dataConnection, string @Email, string @Password, ref int? @UserId, ref string @UserName, ref bool? @IsEmployee)
		{
			var parameters = new []
			{
				new DataParameter("@Email",      @Email,      LinqToDB.DataType.VarChar)
				{
					Size = 150
				},
				new DataParameter("@Password",   @Password,   LinqToDB.DataType.VarChar)
				{
					Size = 255
				},
				new DataParameter("@UserId",     @UserId,     LinqToDB.DataType.Int32)
				{
					Direction = ParameterDirection.InputOutput
				},
				new DataParameter("@UserName",   @UserName,   LinqToDB.DataType.NVarChar)
				{
					Direction = ParameterDirection.InputOutput,
					Size      = 200
				},
				new DataParameter("@IsEmployee", @IsEmployee, LinqToDB.DataType.Boolean)
				{
					Direction = ParameterDirection.InputOutput
				}
			};

			var ret = dataConnection.ExecuteProc("[dbo].[spAuthenticateUser]", parameters);

			@UserId     = Converter.ChangeTypeTo<int?>  (parameters[2].Value);
			@UserName   = Converter.ChangeTypeTo<string>(parameters[3].Value);
			@IsEmployee = Converter.ChangeTypeTo<bool?> (parameters[4].Value);

			return ret;
		}

		#endregion

		#region SpConsultarCobros

		public static IEnumerable<SpConsultarCobrosResult> SpConsultarCobros(this PviProyectoFinalDB dataConnection, string @ClienteNombre, int? @Mes, int? @Anno, int? @IdPersona)
		{
			var parameters = new []
			{
				new DataParameter("@ClienteNombre", @ClienteNombre, LinqToDB.DataType.NVarChar)
				{
					Size = 100
				},
				new DataParameter("@Mes",           @Mes,           LinqToDB.DataType.Int32),
				new DataParameter("@Anno",          @Anno,          LinqToDB.DataType.Int32),
				new DataParameter("@IdPersona",     @IdPersona,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarCobrosResult>("[dbo].[spConsultarCobros]", parameters);
		}

		public partial class SpConsultarCobrosResult
		{
			[Column("id_cobro")   ] public int      Id_cobro    { get; set; }
			[Column("mes")        ] public int?     Mes         { get; set; }
			[Column("anno")       ] public int?     Anno        { get; set; }
			[Column("monto")      ] public decimal? Monto       { get; set; }
			[Column("estado")     ] public string   Estado      { get; set; }
			[Column("nombre_casa")] public string   Nombre_casa { get; set; }
			                        public int?     Id_cliente  { get; set; }
			                        public string   Cliente     { get; set; }
		}

		#endregion

		#region SpCrearCasa

		public static int SpCrearCasa(this PviProyectoFinalDB dataConnection, string @nombreCasa, int? @metrosCuadrados, int? @numeroHabitaciones, int? @numeroBanos, decimal? @precio, int? @idPersona, DateTime? @fechaConstruccion, bool? @estado)
		{
			var parameters = new []
			{
				new DataParameter("@nombre_casa",         @nombreCasa,         LinqToDB.DataType.NVarChar)
				{
					Size = 225
				},
				new DataParameter("@metros_cuadrados",    @metrosCuadrados,    LinqToDB.DataType.Int32),
				new DataParameter("@numero_habitaciones", @numeroHabitaciones, LinqToDB.DataType.Int32),
				new DataParameter("@numero_banos",        @numeroBanos,        LinqToDB.DataType.Int32),
				new DataParameter("@precio",              @precio,             LinqToDB.DataType.Decimal),
				new DataParameter("@id_persona",          @idPersona,          LinqToDB.DataType.Int32),
				new DataParameter("@fecha_construccion",  @fechaConstruccion,  LinqToDB.DataType.Date),
				new DataParameter("@estado",              @estado,             LinqToDB.DataType.Boolean)
			};

			return dataConnection.ExecuteProc("[dbo].[SpCrearCasa]", parameters);
		}

		#endregion

		#region SpCrearCobro

		public static int SpCrearCobro(this PviProyectoFinalDB dataConnection, int? @IdCasa, int? @Mes, int? @Anno, decimal? @Monto, string @ServiciosSeleccionados, int? @IdPersona)
		{
			var parameters = new []
			{
				new DataParameter("@IdCasa",                 @IdCasa,                 LinqToDB.DataType.Int32),
				new DataParameter("@Mes",                    @Mes,                    LinqToDB.DataType.Int32),
				new DataParameter("@Anno",                   @Anno,                   LinqToDB.DataType.Int32),
				new DataParameter("@Monto",                  @Monto,                  LinqToDB.DataType.Decimal),
				new DataParameter("@ServiciosSeleccionados", @ServiciosSeleccionados, LinqToDB.DataType.VarChar)
				{
					Size = -1
				},
				new DataParameter("@IdPersona",              @IdPersona,              LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spCrearCobro]", parameters);
		}

		#endregion

		#region SpCreatediagram

		public static int SpCreatediagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, int? @version, byte[] @definition)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32),
				new DataParameter("@version",     @version,     LinqToDB.DataType.Int32),
				new DataParameter("@definition",  @definition,  LinqToDB.DataType.VarBinary)
				{
					Size = -1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_creatediagram]", parameters);
		}

		#endregion

		#region SpDropdiagram

		public static int SpDropdiagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[sp_dropdiagram]", parameters);
		}

		#endregion

		#region SpEliminarCobro

		public static int SpEliminarCobro(this PviProyectoFinalDB dataConnection, int? @IdCobro, int? @IdPersona)
		{
			var parameters = new []
			{
				new DataParameter("@IdCobro",   @IdCobro,   LinqToDB.DataType.Int32),
				new DataParameter("@IdPersona", @IdPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spEliminarCobro]", parameters);
		}

		#endregion

		#region SpGestionarCasa

		public static int SpGestionarCasa(this PviProyectoFinalDB dataConnection, int? @IdCasa, string @NombreCasa, int? @MetrosCuadrados, int? @NumHabitaciones, int? @NumBanos, int? @IdPersona, DateTime? @FechaConstruccion, bool? @Estado)
		{
			var parameters = new []
			{
				new DataParameter("@IdCasa",            @IdCasa,            LinqToDB.DataType.Int32),
				new DataParameter("@NombreCasa",        @NombreCasa,        LinqToDB.DataType.NVarChar)
				{
					Size = 255
				},
				new DataParameter("@MetrosCuadrados",   @MetrosCuadrados,   LinqToDB.DataType.Int32),
				new DataParameter("@NumHabitaciones",   @NumHabitaciones,   LinqToDB.DataType.Int32),
				new DataParameter("@NumBanos",          @NumBanos,          LinqToDB.DataType.Int32),
				new DataParameter("@IdPersona",         @IdPersona,         LinqToDB.DataType.Int32),
				new DataParameter("@FechaConstruccion", @FechaConstruccion, LinqToDB.DataType.Date),
				new DataParameter("@Estado",            @Estado,            LinqToDB.DataType.Boolean)
			};

			return dataConnection.ExecuteProc("[dbo].[spGestionarCasa]", parameters);
		}

		#endregion

		#region SpGestionarServicio

		public static int SpGestionarServicio(this PviProyectoFinalDB dataConnection, int? @IdServicio, string @Nombre, string @Descripcion, decimal? @Precio, int? @IdCategoria, bool? @Estado)
		{
			var parameters = new []
			{
				new DataParameter("@IdServicio",  @IdServicio,  LinqToDB.DataType.Int32),
				new DataParameter("@Nombre",      @Nombre,      LinqToDB.DataType.NVarChar)
				{
					Size = 100
				},
				new DataParameter("@Descripcion", @Descripcion, LinqToDB.DataType.Text)
				{
					Size = 2147483647
				},
				new DataParameter("@Precio",      @Precio,      LinqToDB.DataType.Decimal),
				new DataParameter("@IdCategoria", @IdCategoria, LinqToDB.DataType.Int32),
				new DataParameter("@Estado",      @Estado,      LinqToDB.DataType.Boolean)
			};

			return dataConnection.ExecuteProc("[dbo].[spGestionarServicio]", parameters);
		}

		#endregion

		#region SpGetServiciosPorCobro

		public static IEnumerable<SpGetServiciosPorCobroResult> SpGetServiciosPorCobro(this PviProyectoFinalDB dataConnection, int? @IdCobro)
		{
			var parameters = new []
			{
				new DataParameter("@IdCobro", @IdCobro, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpGetServiciosPorCobroResult>("[dbo].[spGetServiciosPorCobro]", parameters);
		}

		public partial class SpGetServiciosPorCobroResult
		{
			[Column("id_servicio")] public int    Id_servicio { get; set; }
			[Column("nombre")     ] public string Nombre      { get; set; }
		}

		#endregion

		#region SpHelpdiagramdefinition

		public static IEnumerable<SpHelpdiagramdefinitionResult> SpHelpdiagramdefinition(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpHelpdiagramdefinitionResult>("[dbo].[sp_helpdiagramdefinition]", parameters);
		}

		public partial class SpHelpdiagramdefinitionResult
		{
			[Column("version")   ] public int?   Version    { get; set; }
			[Column("definition")] public byte[] Definition { get; set; }
		}

		#endregion

		#region SpHelpdiagrams

		public static IEnumerable<SpHelpdiagramsResult> SpHelpdiagrams(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname", @diagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",    @ownerId,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpHelpdiagramsResult>("[dbo].[sp_helpdiagrams]", parameters);
		}

		public partial class SpHelpdiagramsResult
		{
			public string Database { get; set; }
			public string Name     { get; set; }
			public int    ID       { get; set; }
			public string Owner    { get; set; }
			public int    OwnerID  { get; set; }
		}

		#endregion

		#region SpInsertarBitacora

		public static int SpInsertarBitacora(this PviProyectoFinalDB dataConnection, int? @IdCobro, int? @IdPersona, string @Detalle)
		{
			var parameters = new []
			{
				new DataParameter("@IdCobro",   @IdCobro,   LinqToDB.DataType.Int32),
				new DataParameter("@IdPersona", @IdPersona, LinqToDB.DataType.Int32),
				new DataParameter("@Detalle",   @Detalle,   LinqToDB.DataType.NVarChar)
				{
					Size = 255
				}
			};

			return dataConnection.ExecuteProc("[dbo].[spInsertarBitacora]", parameters);
		}

		#endregion

		#region SpListarCasas

		public static IEnumerable<SpListarCasasResult> SpListarCasas(this PviProyectoFinalDB dataConnection, bool? @Estado)
		{
			var parameters = new []
			{
				new DataParameter("@Estado", @Estado, LinqToDB.DataType.Boolean)
			};

			return dataConnection.QueryProc<SpListarCasasResult>("[dbo].[spListarCasas]", parameters);
		}

		public partial class SpListarCasasResult
		{
			[Column("id_casa")            ] public int      Id_casa             { get; set; }
			[Column("nombre_casa")        ] public string   Nombre_casa         { get; set; }
			[Column("metros_cuadrados")   ] public int      Metros_cuadrados    { get; set; }
			[Column("numero_habitaciones")] public int      Numero_habitaciones { get; set; }
			[Column("numero_banos")       ] public int      Numero_banos        { get; set; }
			[Column("id_persona")         ] public int      Id_persona          { get; set; }
			[Column("fecha_construccion") ] public DateTime Fecha_construccion  { get; set; }
			[Column("estado")             ] public bool     Estado              { get; set; }
			[Column("propietario_nombre") ] public string   Propietario_nombre  { get; set; }
		}

		#endregion

		#region SpPagarCobro

		public static int SpPagarCobro(this PviProyectoFinalDB dataConnection, int? @IdCobro, DateTime? @FechaPago, int? @IdPersona)
		{
			var parameters = new []
			{
				new DataParameter("@IdCobro",   @IdCobro,   LinqToDB.DataType.Int32),
				new DataParameter("@FechaPago", @FechaPago, LinqToDB.DataType.Date),
				new DataParameter("@IdPersona", @IdPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spPagarCobro]", parameters);
		}

		#endregion

		#region SpRenamediagram

		public static int SpRenamediagram(this PviProyectoFinalDB dataConnection, string @diagramname, int? @ownerId, string @newDiagramname)
		{
			var parameters = new []
			{
				new DataParameter("@diagramname",     @diagramname,    LinqToDB.DataType.NVarChar)
				{
					Size = 128
				},
				new DataParameter("@owner_id",        @ownerId,        LinqToDB.DataType.Int32),
				new DataParameter("@new_diagramname", @newDiagramname, LinqToDB.DataType.NVarChar)
				{
					Size = 128
				}
			};

			return dataConnection.ExecuteProc("[dbo].[sp_renamediagram]", parameters);
		}

		#endregion

		#region SpRetornaCasasActivas

		public static IEnumerable<SpRetornaCasasActivasResult> SpRetornaCasasActivas(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpRetornaCasasActivasResult>("[dbo].[SpRetornaCasasActivas]");
		}

		public partial class SpRetornaCasasActivasResult
		{
			[Column("id_casa")    ] public int    Id_casa     { get; set; }
			[Column("nombre_casa")] public string Nombre_casa { get; set; }
		}

		#endregion

		#region SpRetornaPersona

		public static IEnumerable<SpRetornaPersonaResult> SpRetornaPersona(this PviProyectoFinalDB dataConnection)
		{
			var ms = dataConnection.MappingSchema;

			return dataConnection.QueryProc(dataReader =>
				new SpRetornaPersonaResult
				{
					Id_persona       = Converter.ChangeTypeTo<int>      (dataReader.GetValue(0), ms),
					Nombre           = Converter.ChangeTypeTo<string>   (dataReader.GetValue(1), ms),
					Apellido         = Converter.ChangeTypeTo<string>   (dataReader.GetValue(2), ms),
					Direccion        = Converter.ChangeTypeTo<string>   (dataReader.GetValue(3), ms),
					Email            = Converter.ChangeTypeTo<string>   (dataReader.GetValue(4), ms),
					Telefono         = Converter.ChangeTypeTo<string>   (dataReader.GetValue(5), ms),
					Column7          = Converter.ChangeTypeTo<string>   (dataReader.GetValue(6), ms),
					Fecha_nacimiento = Converter.ChangeTypeTo<DateTime?>(dataReader.GetValue(7), ms),
					Contrasena       = Converter.ChangeTypeTo<string>   (dataReader.GetValue(8), ms),
					Estado           = Converter.ChangeTypeTo<bool?>    (dataReader.GetValue(9), ms),
					Tipo_persona     = Converter.ChangeTypeTo<string>   (dataReader.GetValue(10), ms),
				},
				"[dbo].[spRetornaPersona]");
		}

		public partial class SpRetornaPersonaResult
		{
			[Column("id_persona")      ] public int       Id_persona       { get; set; }
			[Column("nombre")          ] public string    Nombre           { get; set; }
			[Column("apellido")        ] public string    Apellido         { get; set; }
			[Column("direccion")       ] public string    Direccion        { get; set; }
			[Column("email")           ] public string    Email            { get; set; }
			[Column("telefono")        ] public string    Telefono         { get; set; }
			[Column("direccion")       ] public string    Column7          { get; set; }
			[Column("fecha_nacimiento")] public DateTime? Fecha_nacimiento { get; set; }
			[Column("contrasena")      ] public string    Contrasena       { get; set; }
			[Column("estado")          ] public bool?     Estado           { get; set; }
			[Column("tipo_persona")    ] public string    Tipo_persona     { get; set; }
		}

		#endregion

		#region SpRetornaPersonasActivas

		public static IEnumerable<SpRetornaPersonasActivasResult> SpRetornaPersonasActivas(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpRetornaPersonasActivasResult>("[dbo].[SpRetornaPersonasActivas]");
		}

		public partial class SpRetornaPersonasActivasResult
		{
			public int    IdPersona      { get; set; }
			public string NombreCompleto { get; set; }
		}

		#endregion

		#region SpUpgraddiagrams

		public static int SpUpgraddiagrams(this PviProyectoFinalDB dataConnection)
		{
			return dataConnection.ExecuteProc("[dbo].[sp_upgraddiagrams]");
		}

		#endregion
	}

	public static partial class SqlFunctions
	{
		#region FnDiagramobjects

		[Sql.Function(Name="[dbo].[fn_diagramobjects]", ServerSideOnly=true)]
		public static int? FnDiagramobjects()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Bitacora Find(this ITable<Bitacora> table, int IdBitacora)
		{
			return table.FirstOrDefault(t =>
				t.IdBitacora == IdBitacora);
		}

		public static Casa Find(this ITable<Casa> table, int IdCasa)
		{
			return table.FirstOrDefault(t =>
				t.IdCasa == IdCasa);
		}

		public static Categoria Find(this ITable<Categoria> table, int IdCategoria)
		{
			return table.FirstOrDefault(t =>
				t.IdCategoria == IdCategoria);
		}

		public static Cobro Find(this ITable<Cobro> table, int IdCobro)
		{
			return table.FirstOrDefault(t =>
				t.IdCobro == IdCobro);
		}

		public static DetalleCobro Find(this ITable<DetalleCobro> table, int IdServicio, int IdCobro)
		{
			return table.FirstOrDefault(t =>
				t.IdServicio == IdServicio &&
				t.IdCobro    == IdCobro);
		}

		public static Persona Find(this ITable<Persona> table, int IdPersona)
		{
			return table.FirstOrDefault(t =>
				t.IdPersona == IdPersona);
		}

		public static Servicio Find(this ITable<Servicio> table, int IdServicio)
		{
			return table.FirstOrDefault(t =>
				t.IdServicio == IdServicio);
		}
	}
}
