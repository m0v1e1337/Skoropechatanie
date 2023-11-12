namespace Skoropechatanie
{
    class Program
    {
        [Obsolete("Obsolete")]
        static void Main()
        {
            MegalovaniiaPhonk typing = new MegalovaniiaPhonk("", new ConsoleKeyInfo(), 0, null);
            int position = typing.Start();
            string textSelected = typing.GetTextSelected();
            var i = Console.ReadLine();
            var i2 = Convert.ToDouble(Console.ReadLine());
            var i3 = Convert.ToDouble(Console.ReadLine());

            KotuZajali kt = new KotuZajali(i, i2, i3);
            
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            Thread thread = new Thread(() => typing.RecordsList(kt));
            thread.Start();
        }
    }
}