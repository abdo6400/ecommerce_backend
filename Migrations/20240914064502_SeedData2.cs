using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9023d04-f50d-40ae-92f6-03ee301dd38d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb99680c-e5fb-427d-90fa-4a7dbd98c71f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c85c723-3740-48e0-bd85-11c01d197da4", null, "Admin", "ADMIN" },
                    { "fb851173-1364-4489-9fd2-01f0bf676ba1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "http://www.nicolasschroeder.name/shop/films/applet.jsp", "Quo omnis adipisci ipsum et aut totam at impedit voluptatem rerum et.", "Blanditiis iure exercitationem sit sunt fuga maxime magnam voluptatem qui enim est." },
                    { 2, "http://www.lindgorczany.ca/shop/films/page.gem", "Impedit unde consequatur voluptatem facere consequatur consequatur ad quos voluptates assumenda deleniti est.", "Consequuntur vitae velit rem doloribus qui sint inventore autem." },
                    { 3, "http://www.davis.info/interviews/resource.lsp", "Et sit consectetur magnam et omnis error aut.", "Dolorem in est provident quod cupiditate harum ut debitis sit provident accusantium dicta autem." },
                    { 4, "http://www.gottlieb.info/reviews/resource.lsp", "Provident laborum corporis aperiam rerum eaque error rerum molestiae inventore et quidem quo.", "Quam voluptates ipsa odit laudantium sed iure aperiam quis voluptatibus nesciunt odit minima." },
                    { 5, "http://www.bernhard.ca/shop/root.aspx", "Provident libero aut voluptatem fugiat quasi nobis repellendus vero dolorem.", "Iste voluptatibus aperiam et ea corporis dolores quia omnis qui consequatur." }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Image", "NameAr", "NameEn" },
                values: new object[] { 8, 6, "http://www.ratkeblanda.us/home/index.jsp", "Corporis sint est dolores sint alias magni iure quisquam ut quis praesentium eveniet doloribus.", "Quae in voluptas repellat nihil porro perspiciatis repudiandae." });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Image", "NameAr", "NameEn", "SubCategoryId" },
                values: new object[] { 4, "http://www.bednarruecker.com/interviews/applet.jsp", "Heaney, Gottlieb and O'Conner", "Stracke-Moen", 8 });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Image", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, 2, "http://www.prosacco.name/shop/template.gem", "At et deserunt ut omnis cum illum natus eos nemo consequatur.", "Id et exercitationem qui nam eveniet nam iure vel dolores atque quasi ut." },
                    { 2, 2, "http://www.oberbrunner.co.uk/shop/food/template.asp", "Quaerat eveniet et quibusdam quibusdam sit eos beatae est iusto commodi aliquam eum quia.", "Voluptatem sit corrupti corrupti alias beatae commodi quia voluptate quae ad libero eaque." },
                    { 3, 2, "http://www.heaney.us/guide/resource.jsp", "Nam quidem dolorum aut incidunt soluta a consectetur beatae quibusdam incidunt aut nam.", "Accusantium culpa ut eos et aperiam quaerat velit ipsam accusantium pariatur praesentium quod." },
                    { 4, 5, "http://www.beattypouros.biz/shop/books/page.lsp", "Voluptatibus quisquam dicta aspernatur fuga tenetur est nihil sed iure.", "Natus quibusdam est officiis cum dignissimos corrupti a ab dolore." },
                    { 5, 1, "http://www.kilback.uk/films/template.res", "Molestiae alias voluptatem dolore incidunt maxime perferendis quo sit consequatur iste.", "Vel nobis eum recusandae quidem fugit doloribus modi dolorem quia quis." },
                    { 6, 5, "http://www.hansen.us/guide/resource.asp", "Ut enim eos ad perspiciatis repellendus sint error voluptatum.", "Dolorem a qui qui et est quibusdam porro rerum ut esse in." },
                    { 7, 5, "http://www.thompson.us/shop/films/root.rsx", "Ut magni dolor qui aut pariatur sapiente aut rerum animi tempore aut.", "Asperiores reiciendis ea rerum voluptatem et temporibus quo laboriosam impedit dolorum qui." },
                    { 9, 3, "http://www.parisiandickens.ca/shop/food/page.jsp", "Adipisci est nisi non quo omnis quam deleniti at.", "Reiciendis qui aspernatur omnis ut sapiente earum distinctio perspiciatis quisquam aut rerum aliquam." },
                    { 10, 4, "http://www.spencer.name/articles/form.asp", "Tempore autem at dignissimos necessitatibus fugit illum laboriosam quas.", "Nisi fuga ipsum quia rerum tempore animi rerum quia nesciunt ipsa eum vel." }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Image", "NameAr", "NameEn", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, "http://www.pacochacartwright.biz/shop/page.asp", "Schmitt Inc and Sons", "Waelchi-Gulgowski", 9 },
                    { 2, "http://www.lind.biz/shop/page.asp", "Bayer, Kassulke and Runolfsson", "Turner Inc and Sons", 4 },
                    { 3, "http://www.kreiger.co.uk/shop/resource.htm", "Schulist-Dare", "Carter, Bernier and Leannon", 1 },
                    { 5, "http://www.altenwerth.ca/shop/food/form.gem", "Baumbach LLC", "Stokes, Kirlin and McDermott", 2 },
                    { 6, "http://www.nikolaus.us/shop/resource.asp", "McLaughlin, Ankunding and Maggio", "O'Hara-Mitchell", 9 },
                    { 7, "http://www.rodriguezwuckert.name/home/index.gem", "Crooks, Denesik and Jerde", "Hoeger, Bailey and Ward", 3 },
                    { 8, "http://www.shanahan.us/facts/root.html", "Effertz-Roob", "Macejkovic, Hilpert and Kris", 10 },
                    { 9, "http://www.shanahanerdman.co.uk/shop/books/form.rsx", "Lynch, Schinner and D'Amore", "Friesen-Olson", 1 },
                    { 10, "http://www.hoppe.name/shop/films/resource.html", "Dach-Trantow", "Runolfsson Inc and Sons", 9 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "DescriptionAr", "DescriptionEn", "DiscountPercentage", "Images", "MinimumOrderQuantity", "Price", "Product_Unit", "Sku", "Stock", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 11, 4, "Voluptatibus est adipisci non molestiae consequatur tenetur.", "Eum ea neque delectus in.", 20.0, "[\"http://www.kautzermertz.uk/shop/books/template.rsx\"]", 5, 93.0, "unit", "7738", 88, "Commodi nihil cupiditate exercitationem aut natus et ipsum est inventore eaque illum.", "Earum qui suscipit assumenda aut sint voluptatem placeat et eum ut amet qui." },
                    { 12, 4, "Quod nam ipsam beatae eos quam dolores et assumenda.", "Maxime id ea nobis atque non.", 22.0, "[\"http://www.reynoldsblick.info/shop/films/page.gem\"]", 5, 5.0, "unit", "7173", 18, "Amet harum optio quisquam non vitae minus aut molestiae qui aspernatur non laboriosam eaque.", "Velit ab enim at harum ipsum rerum consectetur sed dignissimos quae asperiores omnis." },
                    { 16, 4, "Accusantium quaerat nam hic quasi reiciendis quasi dicta earum dolores.", "Ab suscipit explicabo id quo saepe et enim natus vel.", 11.0, "[\"http://www.oharagrimes.name/articles/resource.rsx\"]", 2, 72.0, "unit", "6274", 29, "Earum iure quo dolor saepe eum sapiente et qui et voluptatum molestias delectus.", "A consequatur tempore dolores ipsa eum reiciendis voluptatem." },
                    { 1, 9, "Et animi hic voluptatem.", "Sed quis amet perferendis aut non ea.", 21.0, "[\"http://www.pagacbartoletti.ca/interviews/root.rsx\"]", 4, 72.0, "unit", "5094", 48, "In ex dignissimos illo ea eum debitis esse rerum ipsa quia eum.", "Esse rerum quisquam nihil consectetur voluptas ea magnam aut reprehenderit est." },
                    { 2, 6, "Et incidunt assumenda rem adipisci deserunt amet ex.", "Rerum quo sit dignissimos odit unde.", 21.0, "[\"http://www.oconnell.uk/catalog/page.rsx\"]", 5, 13.0, "unit", "8169", 168, "Cum est aut officiis sequi quisquam quia similique ad.", "Enim qui voluptatem quo numquam sint non voluptas sed ut voluptatem." },
                    { 3, 10, "Quasi qui consequatur aspernatur ipsum minus ad eius.", "Harum et maxime temporibus aut molestiae et quae.", 10.0, "[\"http://www.mannstamm.com/reviews/template.res\"]", 1, 5.0, "unit", "6822", 62, "Repellendus sunt dolores eum quasi accusamus at quod perferendis animi necessitatibus quia qui.", "Et error nostrum mollitia numquam voluptatem fuga esse quia doloribus ad voluptatibus atque eligendi." },
                    { 4, 2, "Et non magnam nesciunt aut sunt aspernatur.", "Ex omnis iure ad sapiente eum repellat placeat.", 18.0, "[\"http://www.rath.name/shop/form.lsp\"]", 5, 89.0, "unit", "5469", 145, "Nostrum et et suscipit at qui praesentium doloremque numquam sit est at.", "Error totam neque sed et nulla molestiae est doloribus et sit et." },
                    { 5, 8, "Id aut minima consectetur.", "Dolorum vero quas libero similique aliquid.", 22.0, "[\"http://www.carroll.info/home/resource.asp\"]", 2, 77.0, "unit", "2686", 33, "Ratione ut et sint sunt aspernatur alias nisi quos ex quidem.", "Cupiditate officiis voluptates quaerat fugiat ad necessitatibus error voluptate quibusdam." },
                    { 6, 6, "Voluptas quaerat sint consequatur.", "Doloremque praesentium dolorum molestiae nulla dolorem debitis dolorum sint voluptates.", 6.0, "[\"http://www.marquardtlittel.info/articles/index.html\"]", 3, 13.0, "unit", "5227", 178, "Aut beatae voluptas eveniet ipsum incidunt nesciunt vero sint.", "Veniam explicabo aspernatur veritatis natus neque molestiae cumque veritatis quaerat nulla qui nobis est." },
                    { 7, 1, "Sunt soluta voluptas blanditiis quidem aperiam vel at commodi.", "Laboriosam consequuntur quo dicta rem fugit officiis nostrum.", 13.0, "[\"http://www.von.name/catalog/page.jsp\"]", 5, 6.0, "unit", "7396", 169, "Ratione at tenetur possimus repudiandae aperiam aperiam officia et officiis sit.", "Consectetur nostrum assumenda quis dolor sed et ut est dolores deserunt sint quam in." },
                    { 8, 5, "Praesentium commodi qui numquam non.", "Molestiae eos quam laudantium dolor non ea.", 9.0, "[\"http://www.robelokon.info/interviews/applet.aspx\"]", 3, 39.0, "unit", "6724", 76, "Et eum veritatis assumenda labore sint sit suscipit.", "Eveniet doloremque quia exercitationem et rem et inventore nihil ea qui." },
                    { 9, 2, "Quos delectus delectus molestiae tenetur.", "Esse ad et cupiditate vel sed quis voluptas et.", 28.0, "[\"http://www.rippin.biz/interviews/template.asp\"]", 5, 63.0, "unit", "3906", 67, "Repellendus quis occaecati voluptas autem optio voluptate harum laborum.", "Iste illo nostrum odit iusto nisi earum neque." },
                    { 10, 5, "Eum voluptatem labore fugiat qui ea.", "Error sint in tempora reprehenderit fugit distinctio ut iusto libero.", 6.0, "[\"http://www.kuhnromaguera.ca/interviews/applet.asp\"]", 1, 48.0, "unit", "2350", 72, "Blanditiis nam dolor fuga voluptatem corrupti sapiente iste consequatur maxime ab.", "Dolores eaque voluptatem sit error autem culpa atque illum." },
                    { 13, 5, "Molestias perferendis corrupti esse voluptatem nam suscipit illo aspernatur.", "Omnis laboriosam quis est in tempora dolor sunt maiores odit.", 9.0, "[\"http://www.monahan.ca/shop/food/form.rsx\"]", 3, 48.0, "unit", "5027", 181, "Facere veniam excepturi et reprehenderit quaerat mollitia facilis dicta natus et.", "Aut quasi nisi perferendis aut qui placeat minus incidunt ratione porro dignissimos sit dolor." },
                    { 14, 1, "Autem est sapiente sunt non quos.", "Velit nam voluptas architecto.", 17.0, "[\"http://www.rempel.uk/home/form.html\"]", 3, 12.0, "unit", "6791", 51, "Tenetur quasi hic a in dignissimos animi minima repellat iste molestiae doloribus tempora ex.", "Nemo nam nam architecto suscipit iusto expedita dolor fuga provident." },
                    { 15, 9, "Et aut quaerat architecto molestiae qui consequuntur illum iure corporis.", "Velit nostrum excepturi enim voluptas ut occaecati recusandae saepe.", 24.0, "[\"http://www.schultzbednar.ca/shop/food/index.htm\"]", 2, 96.0, "unit", "7670", 192, "Excepturi aperiam sed quia eius eum voluptatem aut ad autem doloremque quae quae cupiditate.", "Perferendis ut et aut nisi ut nobis eos culpa alias voluptas." },
                    { 17, 5, "Dolorem harum aspernatur dolorum.", "Architecto consequatur numquam voluptatem est velit animi occaecati velit.", 9.0, "[\"http://www.gutkowskimills.info/shop/food/root.aspx\"]", 1, 99.0, "unit", "7158", 176, "Eos ad in voluptatum officiis eos maxime dolor consectetur rerum.", "Nesciunt aliquid doloremque asperiores aspernatur non deserunt dolorem." },
                    { 18, 7, "Consequatur optio et alias molestiae autem tempore impedit adipisci.", "Error ab delectus non quibusdam doloremque omnis nostrum quis porro.", 26.0, "[\"http://www.mueller.com/shop/films/template.gem\"]", 4, 74.0, "unit", "4586", 85, "Perferendis voluptatem reiciendis blanditiis velit exercitationem et corrupti quaerat.", "Incidunt quos laboriosam deleniti soluta quos accusamus ut aut voluptatibus temporibus error ex nihil." },
                    { 19, 10, "Vero temporibus molestiae sint omnis est doloremque quod ullam sequi.", "Tempora animi suscipit beatae suscipit delectus est ex.", 8.0, "[\"http://www.emard.com/catalog/applet.res\"]", 2, 26.0, "unit", "7565", 129, "Et id distinctio earum modi pariatur quae explicabo.", "Numquam explicabo voluptatibus harum non esse nulla fugit consequatur autem." },
                    { 20, 1, "Soluta qui hic inventore error est blanditiis maxime omnis.", "Repellendus tempora veniam optio eaque excepturi aut sunt dolore debitis.", 26.0, "[\"http://www.lakin.biz/shop/books/index.res\"]", 3, 16.0, "unit", "2192", 59, "Unde maxime accusantium et totam voluptates facilis nisi et quos labore iure.", "Repellat magnam dolor voluptas corrupti quia in cupiditate incidunt expedita." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c85c723-3740-48e0-bd85-11c01d197da4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb851173-1364-4489-9fd2-01f0bf676ba1");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9023d04-f50d-40ae-92f6-03ee301dd38d", null, "Admin", "ADMIN" },
                    { "eb99680c-e5fb-427d-90fa-4a7dbd98c71f", null, "User", "USER" }
                });
        }
    }
}
