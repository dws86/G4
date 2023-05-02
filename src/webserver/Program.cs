var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//criar as flags de acordo com o tipo de ação.
//Uso geral: script.sh <flag> <user>
//1 - criar usuario. -> script.sh 1 <usuario>
//2 - deletar usuario. -> script.sh 2 <usuario>
//3 - atualizar usuario <senha>. -> script.sh 3 <usuario> <senha>
//4 - desativar usuario. -> script.sh 4 <usuario>
//5 - ativar usuario. -> script.sh 5 <usuario>
//6 - adicionar usuario no grupo. script.sh 6 <usuario> <grupo> (O grupo deve existir).
//7 - Criar Grupo.

//$1 = flag
//$2 = usuario (passwd --status root)
//$3 = senha
//$4 = desativar usuario (chage -l <usuario>)
//$5 = ativar usuario (chage -l <usuario>)
//$6 = adicionar usuario ao grupo. (cat /etc/group).
//$7 = adicionar grupo ao UNIX.

//Criar Usuario flag 1 - Atualizar usuario flag 3 - Adicionar usuario ao grupo
app.MapGet("/flag/{parametro1}/{parametro2}/{parametro3}", async context =>
{
    var t1 = context.Request.RouteValues["parametro1"];
    var t2 = context.Request.RouteValues["parametro2"];
    var t3 = context.Request.RouteValues["parametro3"];
    
System.Diagnostics.ProcessStartInfo process = new System.Diagnostics.ProcessStartInfo();
    process.UseShellExecute = false;
    process.WorkingDirectory = "/bin";
    process.FileName = "sh";
    process.Arguments = $"/ProjetoRedes/script.sh {t1} {t2} {t3}";
        process.RedirectStandardOutput = true;
    System.Diagnostics.Process cmd =  System.Diagnostics.Process.Start(process);
    cmd.WaitForExit();

await context.Response.WriteAsync($"Parametro 1: {t1}. Parametro 2: {t2}. Parametro 3: {t3} ");
});
//Deletar usuario flag2 e desativar usuario se flag 4 e ativar usuario flag 5
app.MapGet("/flag/{parametro1}/{parametro2}", async context =>
{		
    var t1 = context.Request.RouteValues["parametro1"];
    var t2 = context.Request.RouteValues["parametro2"];

System.Diagnostics.ProcessStartInfo process = new System.Diagnostics.ProcessStartInfo();
    process.UseShellExecute = false;
    process.WorkingDirectory = "/bin";
    process.FileName = "sh";
    process.Arguments = $"/ProjetoRedes/script.sh {t1} {t2} ";
        process.RedirectStandardOutput = true;
    System.Diagnostics.Process cmd =  System.Diagnostics.Process.Start(process);
    cmd.WaitForExit();

await context.Response.WriteAsync($"Parametro 1: {t1}. Parametro 2: {t2} ");
});
app.Run();
