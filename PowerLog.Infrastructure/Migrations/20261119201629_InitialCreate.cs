using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerLog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercises", x => x.exercise_id);
                    table.ForeignKey(
                        name: "fk_exercises_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "workouts",
                columns: table => new
                {
                    workout_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    planned = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actual = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workouts", x => x.workout_id);
                    table.ForeignKey(
                        name: "fk_workouts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_records",
                columns: table => new
                {
                    personal_record_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    weight = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    reps = table.Column<int>(type: "integer", nullable: false),
                    rpe = table.Column<int>(type: "integer", nullable: false),
                    record_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personal_records", x => x.personal_record_id);
                    table.ForeignKey(
                        name: "fk_personal_records_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "exercise_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_personal_records_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workout_exercises",
                columns: table => new
                {
                    workout_exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workout_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_exercises", x => x.workout_exercise_id);
                    table.ForeignKey(
                        name: "fk_workout_exercises_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "exercise_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_workout_exercises_workouts_workout_id",
                        column: x => x.workout_id,
                        principalTable: "workouts",
                        principalColumn: "workout_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sets",
                columns: table => new
                {
                    set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workout_exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    set_number = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    reps = table.Column<int>(type: "integer", nullable: false),
                    rpe = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sets", x => x.set_id);
                    table.ForeignKey(
                        name: "fk_sets_workout_exercises_workout_exercise_id",
                        column: x => x.workout_exercise_id,
                        principalTable: "workout_exercises",
                        principalColumn: "workout_exercise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "exercises",
                columns: new[] { "exercise_id", "deleted_at", "description", "is_deleted", "name", "type", "user_id" },
                values: new object[,]
                {
                    { new Guid("c1a7e5d2-4b3f-4e8a-9d61-0f2b7a8c1e01"), null, null, false, "Приседания со штангой", 0, null },
                    { new Guid("c1a7e5d2-4b3f-4e8a-9d61-0f2b7a8c1e02"), null, null, false, "Жим лёжа", 1, null },
                    { new Guid("c1a7e5d2-4b3f-4e8a-9d61-0f2b7a8c1e03"), null, null, false, "Становая тяга", 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_exercises_user_id",
                table: "exercises",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_records_exercise_id",
                table: "personal_records",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_records_user_id",
                table: "personal_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_sets_workout_exercise_id",
                table: "sets",
                column: "workout_exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_exercise_id",
                table: "workout_exercises",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_workout_id",
                table: "workout_exercises",
                column: "workout_id");

            migrationBuilder.CreateIndex(
                name: "ix_workouts_user_id",
                table: "workouts",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personal_records");

            migrationBuilder.DropTable(
                name: "sets");

            migrationBuilder.DropTable(
                name: "workout_exercises");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "workouts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
