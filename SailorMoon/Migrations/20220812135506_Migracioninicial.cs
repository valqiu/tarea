using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SailorMoon.Migrations
{
    public partial class Migracioninicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    id_curso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_curso = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__curso__5D3F7502EB9E7BF4", x => x.id_curso);
                });

            migrationBuilder.CreateTable(
                name: "grado",
                columns: table => new
                {
                    id_grado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grado = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    seccion = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    tutor = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__grado__6DB797EE553FC898", x => x.id_grado);
                });

            migrationBuilder.CreateTable(
                name: "institucion",
                columns: table => new
                {
                    id_institucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: true),
                    nivel = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    codigo_modular = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    logo = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__instituc__680718D3D39D9159", x => x.id_institucion);
                });

            migrationBuilder.CreateTable(
                name: "tipo_usuario",
                columns: table => new
                {
                    id_tipo_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tipo_usu__B17D78C8E2DB4D10", x => x.id_tipo_usuario);
                });

            migrationBuilder.CreateTable(
                name: "estudiante",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    dni = table.Column<int>(type: "int", nullable: true),
                    carnet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    codigo_est = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_grado_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__estudian__E0B2763C131F02AF", x => x.id_estudiante);
                    table.ForeignKey(
                        name: "FK__estudiant__id_gr__286302EC",
                        column: x => x.id_grado_fk,
                        principalTable: "grado",
                        principalColumn: "id_grado");
                });

            migrationBuilder.CreateTable(
                name: "personal",
                columns: table => new
                {
                    id_personal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    usuario = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    clave = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    id_tipo_usuario = table.Column<int>(type: "int", nullable: true),
                    id_curso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personal__418FB8085ED2EB8C", x => x.id_personal);
                    table.ForeignKey(
                        name: "FK__personal__id_cur__32E0915F",
                        column: x => x.id_curso,
                        principalTable: "curso",
                        principalColumn: "id_curso");
                    table.ForeignKey(
                        name: "FK__personal__id_tip__31EC6D26",
                        column: x => x.id_tipo_usuario,
                        principalTable: "tipo_usuario",
                        principalColumn: "id_tipo_usuario");
                });

            migrationBuilder.CreateTable(
                name: "asistencias",
                columns: table => new
                {
                    id_asistencias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inast_justificada = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    inast_injustificada = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    tard_justificada = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    tard_injustificada = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    id_estudiante = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asistenc__B74A34E3F86B36F4", x => x.id_asistencias);
                    table.ForeignKey(
                        name: "FK__asistenci__id_es__2B3F6F97",
                        column: x => x.id_estudiante,
                        principalTable: "estudiante",
                        principalColumn: "id_estudiante");
                });

            migrationBuilder.CreateTable(
                name: "conduccion_descriptiva",
                columns: table => new
                {
                    id_conduccion_descriptiva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    periodo = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    id_estudiante = table.Column<int>(type: "int", nullable: true),
                    id_personal = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__conducci__E2291AAA4A171E4F", x => x.id_conduccion_descriptiva);
                    table.ForeignKey(
                        name: "FK__conduccio__id_pe__35BCFE0A",
                        column: x => x.id_estudiante,
                        principalTable: "estudiante",
                        principalColumn: "id_estudiante");
                });

            migrationBuilder.CreateTable(
                name: "notas",
                columns: table => new
                {
                    id_notas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nota1 = table.Column<double>(type: "float", nullable: true),
                    nota2 = table.Column<double>(type: "float", nullable: true),
                    nota3 = table.Column<double>(type: "float", nullable: true),
                    nota4 = table.Column<double>(type: "float", nullable: true),
                    promedio = table.Column<double>(type: "float", nullable: true),
                    id_Curso = table.Column<int>(type: "int", nullable: true),
                    id_estudiante_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notas__832F7CFAC639F399", x => x.id_notas);
                    table.ForeignKey(
                        name: "FK__notas__id_Curso__38996AB5",
                        column: x => x.id_Curso,
                        principalTable: "curso",
                        principalColumn: "id_curso");
                    table.ForeignKey(
                        name: "FK__notas__id_estudi__398D8EEE",
                        column: x => x.id_estudiante_fk,
                        principalTable: "estudiante",
                        principalColumn: "id_estudiante");
                });

            migrationBuilder.CreateTable(
                name: "notas_periodo",
                columns: table => new
                {
                    id_periodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nota_periodo1 = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    nota_periodo2 = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    nota_periodo3 = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    nota_periodo4 = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    promedio_anual = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    id_notas = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notas_pe__801188B7406FD76B", x => x.id_periodo);
                    table.ForeignKey(
                        name: "FK__notas_per__id_no__3C69FB99",
                        column: x => x.id_notas,
                        principalTable: "notas",
                        principalColumn: "id_notas");
                });

            migrationBuilder.CreateIndex(
                name: "IX_asistencias_id_estudiante",
                table: "asistencias",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_conduccion_descriptiva_id_estudiante",
                table: "conduccion_descriptiva",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_estudiante_id_grado_fk",
                table: "estudiante",
                column: "id_grado_fk");

            migrationBuilder.CreateIndex(
                name: "IX_notas_id_Curso",
                table: "notas",
                column: "id_Curso");

            migrationBuilder.CreateIndex(
                name: "IX_notas_id_estudiante_fk",
                table: "notas",
                column: "id_estudiante_fk");

            migrationBuilder.CreateIndex(
                name: "IX_notas_periodo_id_notas",
                table: "notas_periodo",
                column: "id_notas");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_curso",
                table: "personal",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_personal_id_tipo_usuario",
                table: "personal",
                column: "id_tipo_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asistencias");

            migrationBuilder.DropTable(
                name: "conduccion_descriptiva");

            migrationBuilder.DropTable(
                name: "institucion");

            migrationBuilder.DropTable(
                name: "notas_periodo");

            migrationBuilder.DropTable(
                name: "personal");

            migrationBuilder.DropTable(
                name: "notas");

            migrationBuilder.DropTable(
                name: "tipo_usuario");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "estudiante");

            migrationBuilder.DropTable(
                name: "grado");
        }
    }
}
