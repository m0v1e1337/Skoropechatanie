using System.Diagnostics;
using Newtonsoft.Json;

namespace Skoropechatanie;

class MegalovaniiaPhonk
{
    private readonly string[] _texts = {
        "Этот вид спорта берет свое начало в 1891 году в городе Спрингфилд, штат Массачусетс, США. " +
        "Его создал физрук Джеймс Нейсмит для использования в качестве урока для учеников внутри помещения в холодное время года. " +
        "Первоначальные правила были простыми: игроки должны забрасывать мяч в кольцо, которое было прикреплено к стене, на высоте 3,05 метра. " +
        "Каждый удачный бросок давал один балл."
    };
    private string _textSelected;
    private Random _random = new Random();
    private ConsoleKeyInfo _key;
    private int _position;
    private Thread _thread;

    public MegalovaniiaPhonk(string textSelected, ConsoleKeyInfo key, int position, Thread thread)
    {
        _textSelected = textSelected;
        _key = key;
        _position = position;
        _thread = thread;
    }

    public string GetTextSelected()
    {
        return _textSelected;
    }

    [Obsolete("Obsolete")]
    public int Start()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _textSelected = _texts[_random.Next(_texts.Length)];
        Console.WriteLine(_textSelected + "\n--------------------\nКогда будете готовы нажмите Enter");
        Console.ReadLine();

        void ThreadStart()
        {
            stopwatch.Restart();
            while (true)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000 + "/60");
                Thread.Sleep(1000);
            }
        }

        _thread = new Thread(ThreadStart);
        _thread.Start();
        while (_thread.IsAlive && stopwatch.ElapsedMilliseconds < 60000)
        {
            Console.SetCursorPosition(0, 11);
            Console.Write("        ");
            Console.SetCursorPosition(0, 11);
            _key = Console.ReadKey(true);
            if (_position < _textSelected.Length && _textSelected[_position] == _key.KeyChar)
            {
                Console.SetCursorPosition(_position - (_position/120)*120, _position/120);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(_textSelected[_position]);
                Console.ResetColor();
                _position++;
            }
            _key = Console.ReadKey(false);
            if (_position < _textSelected.Length && _textSelected[_position] != _key.KeyChar)
            {
                Console.SetCursorPosition(_position - (_position/120)*120, _position/120);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_textSelected[_position]);
                Console.ResetColor();
                _position++;
            }
        }
        _thread.Abort();
        Console.SetCursorPosition(0, 10);
        Console.WriteLine("Stop");
        Thread.Sleep(3000);
        return _position;
    }

    public void RecordsList(KotuZajali result)
    {
        List<KotuZajali>? list = new List<KotuZajali>();
        if (File.Exists("C:\\Users\\Dmitrii Tihonov\\Desktop\\Я непогрешим\\Лекуия пиздострадания\\София Алексеевна\\Records.json"))
        {
            string json = File.ReadAllText("C:\\Users\\Dmitrii Tihonov\\Desktop\\Я непогрешим\\Лекуия пиздострадания\\София Алексеевна\\Records.json");
            list = JsonConvert.DeserializeObject<List<KotuZajali>>(json);
        }

        if (list != null)
        {
            list.Add(result);

            string updatedJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(
                "C:\\Users\\Dmitrii Tihonov\\Desktop\\Я непогрешим\\Лекуия пиздострадания\\София Алексеевна\\Records.json",
                updatedJson);

            Console.WriteLine("Таблица рекордов:\n--------------");
            foreach (KotuZajali item in list)
            {
                Console.WriteLine(item.Name + "    " + item.AmountPerMin + " символов в минуту   " + item.AmountPerSec +
                                  " символов в секунду");
            }
        }

        Console.WriteLine("Нажмите Enter для продолжения");
        Console.ReadLine();
    }

}