namespace OpenDataStructuresCsharp;

[TestClass]
public class Chapter1
{
    // 以下の問題は、テキストの入力を1行ずつ読み、各業で適切なデータ構造の操作を実行することで解いてほしい。
    // ファイルが百万行であっても数秒以内に処理できる程度の効率的な実装にすること

    private static TestContext _context;

    private static readonly string s_pathName = Path.Combine(Environment.GetEnvironmentVariable("HOME"),
        "RiderProjects/OpenDataStructuresCsharp/OpenDataStructuresCsharp/ilias03ilias.txt");

    [ClassInitialize]
    public static void ClassInit(TestContext context) => _context = context;

    /// <summary>
    ///     入力を1行ずつ読み を実現する
    /// </summary>
    /// <returns></returns>
    private IEnumerable<string> ReadLines()
    {
        using StreamReader sr = new(s_pathName);
        for (;;)
        {
            string? line = sr.ReadLine();
            if (line == null)
            {
                yield break;
            }

            yield return line;
        }
    }

    /// <summary>
    ///     入力を1行ずつ読み、その逆順で出力せよ。すなわち、最後の入力行を最初に書き出し、最後から二行目を2番目に書き出す、というように出力せよ
    /// </summary>
    [TestMethod]
    public void Q1_1_1()
    {
        // この設問は「ファイルが百万行であっても数秒以内に処理できる程度の効率的な実装」実現可能なのだろうか?
        foreach (string line in ReadLines().Reverse())
        {
            _context.WriteLine(line);
        }
    }

    /// <summary>
    ///     先頭から50行の入力を読み、それを逆順で出力せよ。
    ///     その後、続く50行を読み、それを逆順で出力せよ。
    ///     これを読み取る行がなくなるまで繰り返し、最後に残った行(50行未満かもしれない) もやはり逆順で出力せよ
    /// </summary>
    [TestMethod]
    public void Q1_1_2()
    {
        foreach (IList<string> lines50 in ReadLines().Buffer(50))
        {
            foreach (string line in lines50.Reverse())
            {
                _context.WriteLine(line);
            }
        }
    }

    /// <summary>
    ///     入力を1行ずつ読み取り、42行目以降で空行を見つけたら、その42行前の行を出力せよ。
    ///     例えば242行目が空行であれば200行目を出力せよ。なお、プログラムの実行中に43行以上の行を保持してはならない。
    /// </summary>
    [TestMethod]
    public void Q1_1_3()
    {
        const int queueSize = 42;
        Queue<string> queue = new(queueSize);
        foreach (string line in ReadLines())
        {
            queue.Enqueue(line);
            if (queue.Count > queueSize)
            {
                string prev42 = queue.Dequeue();
                Assert.AreEqual(42, queue.Count);
                if (string.IsNullOrEmpty(line))
                {
                    _context.WriteLine(prev42); // 42行前の行を出力せよ
                }
            }
        }
    }

    /// <summary>
    ///     入力を1行ずつ読み取り、それまでに読み込んだことがある行と重複しない行を見つけたら出力せよ。
    ///     重複が多いファイルを読む場合でも、重複なく行を保持するのに必要なメモリより多くのメモリを使わないように注意せよ
    /// </summary>
    [TestMethod]
    public void Q1_1_4()
    {
        HashSet<string> uniqueLines = new();
        foreach (string line in ReadLines())
        {
            if (uniqueLines.Contains(line))
            {
                ;
            }
            else
            {
                _context.WriteLine(line);
                uniqueLines.Add(line);
            }
        }
    }

    /// <summary>
    ///     入力を1行ずつ読み取り、それまでに読み込んだことがある行と同じなら出力せよ。
    ///     最終的にはある行が入力ファイルに初めて現れた箇所をそれぞれ除いたものが出力になる。
    ///     重複が多いファイルを読む場合でも、重複なく行を保持するのに必要なメモリより多くのメモリを使わないように注意せよ
    /// </summary>
    [TestMethod]
    public void Q1_1_5()
    {
        HashSet<string> uniqueLines = new();
        foreach (string line in ReadLines())
        {
            if (uniqueLines.Contains(line))
            {
                _context.WriteLine(line);
            }
            else
            {
                uniqueLines.Add(line);
            }
        }
    }

    /// <summary>
    ///     入力をすべて読み取り、短い順に並べ替えて出力せよ。同じ長さの行があるときは、それらの行は辞書順に並べるものとする。
    ///     また、重複する行は一度だけ出力するものとする
    /// </summary>
    [TestMethod]
    public void Q1_1_6()
    {
        foreach (string line in ReadLines()
                     .Distinct() // 設問に登場する順序とは異なり、Distinct は順序を保証しないため先に実行しなければならない
                     .OrderBy(x => x))
        {
            // Distinct+OrderBy は内部で同時に解釈して効率的に実行される可能性について 確認する価値がある
            _context.WriteLine(line);
        }
    }

    /// <summary>
    ///     直前の問題で、重複する行については現れた回数だけ出力するように変更せよ。
    /// </summary>
    [TestMethod]
    public void Q1_1_7()
    {
        foreach (string line in ReadLines()
                     .OrderBy(x => x))
        {
            _context.WriteLine(line);
        }
    }

    /// <summary>
    ///     入力をすべて読み取り、すべての偶数番目の行を出力したあとに、すべての奇数番目の行を出力せよ。最初の行を0行目と数える
    /// </summary>
    [TestMethod]
    public void Q1_1_8()
    {
        List<string> oddLines = new();
        int cnt = 0;
        foreach (string line in ReadLines())
        {
            if (cnt % 2 == 0)
            {
                _context.WriteLine(line);
            }
            else
            {
                oddLines.Add(line);
            }

            cnt++;
        }

        foreach (string line in oddLines)
        {
            _context.WriteLine(line);
        }
    }

    [TestMethod]
    public void Q1_1_8a()
    {
        // 対象ファイルが大きい想定であればメモリ効率的にはファイルを2回読む方がよいのではないか？
        void Proc(int evenOrOdd)
        {
            int cnt = 0;
            foreach (string line in ReadLines())
            {
                if (cnt % 2 == evenOrOdd)
                {
                    _context.WriteLine(line);
                }

                cnt++;
            }
        }

        Proc(0);
        Proc(1);
    }

    /// <summary>
    ///     入力をすべて読み取り、ランダムに並べ替えて出力せよ。どの行の内容も書き換えてはならない。
    ///     また、入力より行が増えたり減ったりしてもいけない。
    /// </summary>
    [TestMethod]
    public void Q1_1_9()
    {
        Random? rand = new(0 /* seed 固定 */);

        string RandomDequeue(IList<string> lines)
        {
            int idx = rand.Next(lines.Count);
            string line = lines[idx];
            lines.RemoveAt(idx);
            return line;
        }

        // RemoveAt を使った時に全データアロケートしなおししないよう ToArray は使わない
        for (List<string> lines = ReadLines().ToList();
             lines.Any();
            )
        {
            string line = RandomDequeue(lines);
            _context.WriteLine(line);
        }
    }

    [TestMethod]
    public void Q1_6_list()
    {
        Chapter1List<string> list = new();
        Assert.AreEqual(0, list.size());
        list.add(0, "a");
        Assert.AreEqual(1, list.size());
        Assert.AreEqual("a", list.get(0));
        list.set(0, "A");
        Assert.AreEqual(1, list.size());
        Assert.AreEqual("A", list.get(0));
        list.add(1, "b");
        Assert.AreEqual(2, list.size());
        Assert.AreEqual("A", list.get(0));
        Assert.AreEqual("b", list.get(1));
        list.add(2, "c");
        Assert.AreEqual(3, list.size());
        Assert.AreEqual("A", list.get(0));
        Assert.AreEqual("b", list.get(1));
        Assert.AreEqual("c", list.get(2));
        list.remove(1);
        Assert.AreEqual(2, list.size());
        Assert.AreEqual("A", list.get(0));
        Assert.AreEqual("c", list.get(1));
    }

    [TestMethod]
    public void Q1_6_uset()
    {
        Chapter1USet<string> uset = new();
        Assert.AreEqual(0, uset.size());
        Assert.AreEqual(null, uset.find("a"));

        uset.add("a");
        Assert.AreEqual(1, uset.size());
        Assert.AreEqual("a", uset.find("a"));
        uset.add("A");
        Assert.AreEqual(2, uset.size());
        Assert.AreEqual("a", uset.find("a"));
        Assert.AreEqual("A", uset.find("A"));
        Assert.AreEqual(null, uset.find("b"));
        uset.add("b");
        Assert.AreEqual(3, uset.size());
        Assert.AreEqual("a", uset.find("a"));
        Assert.AreEqual("A", uset.find("A"));
        Assert.AreEqual("b", uset.find("b"));
        uset.add("a");
        Assert.AreEqual(3, uset.size());
        Assert.AreEqual("a", uset.find("a"));
        Assert.AreEqual("A", uset.find("A"));
        Assert.AreEqual("b", uset.find("b"));
        uset.remove("a");
        Assert.AreEqual(2, uset.size());
        Assert.AreEqual(null, uset.find("a"));
        Assert.AreEqual("A", uset.find("A"));
        Assert.AreEqual("b", uset.find("b"));
        uset.remove("c");
        Assert.AreEqual(2, uset.size());
        Assert.AreEqual(null, uset.find("a"));
        Assert.AreEqual("A", uset.find("A"));
        Assert.AreEqual("b", uset.find("b"));
        Assert.AreEqual(null, uset.find("Z"));
        Assert.AreEqual(null, uset.find("z"));
    }

    [TestMethod]
    public void Q1_6_sset()
    {
        // 文字列の比較は　Culture によって複雑な挙動をする。特に 大文字と小文字 の比較は直感に反するため 以下では小文字のみ扱う
        Assert.AreEqual(-1, "a".CompareTo("c"));


        Chapter1SSet<string> sset = new();
        Assert.AreEqual(0, sset.size());
        Assert.AreEqual(null, sset.find("a"));

        sset.add("c");
        Assert.AreEqual(1, sset.size());
        Assert.AreEqual("c", sset.find("a"));
        Assert.AreEqual("c", sset.find("b"));
        Assert.AreEqual("c", sset.find("c"));
        Assert.AreEqual(null, sset.find("d"));
        Assert.AreEqual(null, sset.find("e"));
        Assert.AreEqual(null, sset.find("z"));
        sset.add("e");
        Assert.AreEqual(2, sset.size());
        Assert.AreEqual("c", sset.find("a"));
        Assert.AreEqual("c", sset.find("b"));
        Assert.AreEqual("c", sset.find("c"));
        Assert.AreEqual("e", sset.find("d"));
        Assert.AreEqual("e", sset.find("e"));
        Assert.AreEqual(null, sset.find("z"));
        sset.add("b");
        Assert.AreEqual(3, sset.size());
        Assert.AreEqual("b", sset.find("a"));
        Assert.AreEqual("b", sset.find("b"));
        Assert.AreEqual("c", sset.find("c"));
        Assert.AreEqual("e", sset.find("d"));
        Assert.AreEqual("e", sset.find("e"));
        Assert.AreEqual(null, sset.find("z"));
        sset.remove("b");
        Assert.AreEqual(2, sset.size());
        Assert.AreEqual("c", sset.find("a"));
        Assert.AreEqual("c", sset.find("b"));
        Assert.AreEqual("c", sset.find("c"));
        Assert.AreEqual("e", sset.find("d"));
        Assert.AreEqual("e", sset.find("e"));
        Assert.AreEqual(null, sset.find("z"));
        sset.remove("z");
        Assert.AreEqual(2, sset.size());
        Assert.AreEqual("c", sset.find("a"));
        Assert.AreEqual("c", sset.find("b"));
        Assert.AreEqual("c", sset.find("c"));
        Assert.AreEqual("e", sset.find("d"));
        Assert.AreEqual("e", sset.find("e"));
        Assert.AreEqual(null, sset.find("z"));
    }
}

// 問1.6 最も簡単なものは要素を配列に入れておく方法だ
internal class Chapter1List<T> : IMyList<T> where T : class, IEquatable<T>
{
    private T[] _items = Array.Empty<T>();

    public int size() => _items.Length;

    public T get(int i) => _items[i];

    public void set(int i, T x) => _items[i] = x;

    public void add(int i, T x)
    {
        T[] addedItems = new T[_items.Length + 1];
        ItemCopy(0, 0, i, _items, addedItems);
        addedItems[i] = x;
        ItemCopy(i, i + 1, _items.Length - i, _items, addedItems);
        // ここで以前の _items は捨てたい
        _items = addedItems;
    }

    public void remove(int i)
    {
        T[] removedItems = new T[_items.Length - 1];
        ItemCopy(0, 0, i, _items, removedItems);
        // src の i 番目を捨てる
        ItemCopy(i + 1, i, _items.Length - i - 1, _items, removedItems);
        // ここで以前の _items は捨てたい
        _items = removedItems;
    }

    private static void ItemCopy(int srcStart, int destStart, int length, T[] source, T[] destination)
    {
        if (length < 0
            || srcStart < 0
            || destStart < 0
            || srcStart + length > source.Length
            || destStart + length > destination.Length
           )
        {
            // ItemCopy は境界エラーに対して 静かに無視している。GO 言語流儀で エラーの原因を返すのが望ましい
            return;
        }

        source.AsSpan(srcStart, length).CopyTo(destination.AsSpan(destStart, length));
    }
}

internal class Chapter1USet<T> : IUSet<T> where T : class, IEquatable<T>
{
    private T[] _items = Array.Empty<T>();

    public int size() => _items.Length;

    public T? find(T x) => _items.Contains(x) ? x : null;

    public void add(T x)
    {
        if (find(x) != null)
        {
            return;
        }
        // foreach でコピーしながら 一致するアイテムを探す方法を検討したが
        // 先に存在確認をして「操作しなくてよい」が決まる方が良い

        T[] addedItems = new T[_items.Length + 1];
        addedItems[0] = x;
        ItemCopy(0, 1, _items.Length, _items, addedItems);

        // ここで以前の _items は捨てたい
        _items = addedItems;
    }

    public void remove(T x)
    {
        // 対象がない場合には操作してはいけない
        if (find(x) == null)
        {
            return;
        }

        T[] removedItems = new T[_items.Length - 1];
        int removedIndex = 0;
        foreach (T item in _items)
        {
            // ここは各要素比較するしかない
            if (!item.Equals(x))
            {
                removedItems[removedIndex] = item;
                removedIndex++;
            }
        }

        _items = removedItems;
    }

    private static void ItemCopy(int srcStart, int destStart, int length, T[] source, T[] destination)
    {
        if (length < 0
            || srcStart < 0
            || destStart < 0
            || srcStart + length > source.Length
            || destStart + length > destination.Length
           )
        {
            // ItemCopy は境界エラーに対して 静かに無視している。GO 言語流儀で エラーの原因を返すのが望ましい
            return;
        }

        source.AsSpan(srcStart, length).CopyTo(destination.AsSpan(destStart, length));
    }
}

internal class Chapter1SSet<T> : ISSet<T> where T : class, IComparable<T>
{
    private T[] _items = Array.Empty<T>();

    public int size() => _items.Length;

    public T? find(T x)
    {
        // ここでは _items がソート済みでない配列という実装
        // add の時点でソート済みなら BinarySearch が使える
        // remove の時点で二分木のバランス調整が必要かもしれない というアイデア
        T? result = null;
        foreach (T item in _items)
        {
            if (item.CompareTo(x) >= 0
                && (result == null || result.CompareTo(item) > 0))
            {
                result = item;
            }
        }

        return result;
    }

    public void add(T x)
    {
        if (find(x) == x) // null との比較でないのは USet と違う
        {
            return;
        }
        // foreach でコピーしながら 一致するアイテムを探す方法を検討したが
        // 先に存在確認をして「操作しなくてよい」が決まる方が良い

        T[] addedItems = new T[_items.Length + 1];
        addedItems[0] = x;
        ItemCopy(0, 1, _items.Length, _items, addedItems);

        // ここで以前の _items は捨てたい
        _items = addedItems;
    }

    public void remove(T x)
    {
        // 対象がない場合には操作してはいけない
        if (find(x) != x) // null との比較でないのは USet と違う
        {
            return;
        }

        T[] removedItems = new T[_items.Length - 1];
        int removedIndex = 0;
        foreach (T item in _items)
        {
            // ここは各要素比較するしかない
            if (!item.Equals(x))
            {
                removedItems[removedIndex] = item;
                removedIndex++;
            }
        }

        _items = removedItems;
    }

    private static void ItemCopy(int srcStart, int destStart, int length, T[] source, T[] destination)
    {
        if (length < 0
            || srcStart < 0
            || destStart < 0
            || srcStart + length > source.Length
            || destStart + length > destination.Length
           )
        {
            // ItemCopy は境界エラーに対して 静かに無視している。GO 言語流儀で エラーの原因を返すのが望ましい
            return;
        }

        source.AsSpan(srcStart, length).CopyTo(destination.AsSpan(destStart, length));
    }
}
