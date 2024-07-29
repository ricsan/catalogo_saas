using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MD3.CatalogoSaaS.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGeral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PaiId = table.Column<int>(name: "Pai_Id", type: "int", nullable: true),
                    PaiId0 = table.Column<int>(name: "PaiId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Tenants_PaiId",
                        column: x => x.PaiId0,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DataDeCadastro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IdpIds = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosDosSistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SistemaId = table.Column<int>(name: "Sistema_Id", type: "int", nullable: true),
                    CodigoUnico = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NivelDeConta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosDosSistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametrosDosSistemas_Sistemas_Sistema_Id",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosDosSistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SistemaId = table.Column<int>(name: "Sistema_Id", type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosDosSistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosDosSistemas_Sistemas_Sistema_Id",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesDeSistemas",
                columns: table => new
                {
                    SistemaId = table.Column<int>(name: "Sistema_Id", type: "int", nullable: false),
                    ParametroId = table.Column<int>(name: "Parametro_Id", type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesDeSistemas", x => new { x.SistemaId, x.ParametroId });
                    table.ForeignKey(
                        name: "FK_ConfiguracoesDeSistemas_ParametrosDosSistemas_Parametro_Id",
                        column: x => x.ParametroId,
                        principalTable: "ParametrosDosSistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesDeSistemas_Sistemas_Sistema_Id",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContasDeSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(name: "Tenant_Id", type: "int", nullable: true),
                    SistemaId = table.Column<int>(name: "Sistema_Id", type: "int", nullable: true),
                    PlanoId = table.Column<int>(name: "Plano_Id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasDeSistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasDeSistema_PlanosDosSistemas_Plano_Id",
                        column: x => x.PlanoId,
                        principalTable: "PlanosDosSistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContasDeSistema_Sistemas_Sistema_Id",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContasDeSistema_Tenants_Tenant_Id",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesDeContas",
                columns: table => new
                {
                    ContaId = table.Column<int>(name: "Conta_Id", type: "int", nullable: false),
                    ParametroId = table.Column<int>(name: "Parametro_Id", type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesDeContas", x => new { x.ContaId, x.ParametroId });
                    table.ForeignKey(
                        name: "FK_ConfiguracoesDeContas_ContasDeSistema_Conta_Id",
                        column: x => x.ContaId,
                        principalTable: "ContasDeSistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesDeContas_ParametrosDosSistemas_Parametro_Id",
                        column: x => x.ParametroId,
                        principalTable: "ParametrosDosSistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelUsuarioContaDeSistema",
                columns: table => new
                {
                    ContaDeSistemaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelUsuarioContaDeSistema", x => new { x.ContaDeSistemaId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_RelUsuarioContaDeSistema_ContasDeSistema_ContaDeSistemaId",
                        column: x => x.ContaDeSistemaId,
                        principalTable: "ContasDeSistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelUsuarioContaDeSistema_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesDeContas_Parametro_Id",
                table: "ConfiguracoesDeContas",
                column: "Parametro_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesDeSistemas_Parametro_Id",
                table: "ConfiguracoesDeSistemas",
                column: "Parametro_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContasDeSistema_Plano_Id",
                table: "ContasDeSistema",
                column: "Plano_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContasDeSistema_Sistema_Id",
                table: "ContasDeSistema",
                column: "Sistema_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContasDeSistema_Tenant_Id",
                table: "ContasDeSistema",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosDosSistemas_Sistema_Id_CodigoUnico",
                table: "ParametrosDosSistemas",
                columns: new[] { "Sistema_Id", "CodigoUnico" },
                unique: true,
                filter: "[Sistema_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosDosSistemas_Sistema_Id",
                table: "PlanosDosSistemas",
                column: "Sistema_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RelUsuarioContaDeSistema_UsuarioId",
                table: "RelUsuarioContaDeSistema",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sistemas_CodigoInterno",
                table: "Sistemas",
                column: "CodigoInterno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PaiId",
                table: "Tenants",
                column: "PaiId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesDeContas");

            migrationBuilder.DropTable(
                name: "ConfiguracoesDeSistemas");

            migrationBuilder.DropTable(
                name: "RelUsuarioContaDeSistema");

            migrationBuilder.DropTable(
                name: "ParametrosDosSistemas");

            migrationBuilder.DropTable(
                name: "ContasDeSistema");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "PlanosDosSistemas");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Sistemas");
        }
    }
}
