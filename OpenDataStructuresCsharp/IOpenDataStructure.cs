namespace OpenDataStructuresCsharp;

public interface IMyList<T> where T : class, IEquatable<T>
{
    /// <summary>
    ///     リストの長さ n を返す
    /// </summary>
    /// <returns></returns>
    int size();

    /// <summary>
    ///     xi の値を返す
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    T get(int i);

    /// <summary>
    ///     xi の値を x にする
    /// </summary>
    /// <param name="i"></param>
    /// <param name="x"></param>
    void set(int i, T x);

    /// <summary>
    ///     x を i 番目(0 origin)として追加し、xi,...,xn-1 を後ろにずらす
    /// </summary>
    /// <param name="i"></param>
    /// <param name="x"></param>
    void add(int i, T x);

    /// <summary>
    ///     xi を削除し、xi+1,...,xn-1 を前にずらす。
    /// </summary>
    /// <param name="i"></param>
    void remove(int i);
}

// find() が null を返す、というインターフェース のため　T : class が必要になった。
// List には必要ではないのだが書籍の前提が 1ワード 32bit or 64bit ということもあり struct は扱わない
public interface IUSet<T> where T : class, IEquatable<T>
{
    /// <summary>
    ///     リストの長さ n を返す
    /// </summary>
    /// <returns></returns>
    int size();

    /// <summary>
    ///     要素x が集合に入っていなければ集合に追加する
    /// </summary>
    /// <param name="x"></param>
    void add(T x);

    /// <summary>
    ///     集合からx を削除する
    /// </summary>
    /// <param name="i"></param>
    void remove(T i);

    /// <summary>
    ///     集合にxが放っていればそれを見つける。見つからなければnullを返す
    /// </summary>
    T? find(T x);
}

public interface ISSet<T> where T : class, IComparable<T>
{
    /// <summary>
    ///     リストの長さ n を返す
    /// </summary>
    /// <returns></returns>
    int size();

    /// <summary>
    ///     要素x が集合に入っていなければ集合に追加する
    /// </summary>
    /// <param name="x"></param>
    void add(T x);

    /// <summary>
    ///     集合からx を削除する
    /// </summary>
    void remove(T x);

    /// <summary>
    ///     順序付けられた集合から x の一を特定する。y ≧ x を満たす最小の要素yを見つける。存在しないなら null を返す
    ///     後継探索 (successor search) と呼ばれることがある
    /// </summary>
    T? find(T x);
}
