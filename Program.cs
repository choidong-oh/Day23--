using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading;

public class Edge<T> 
{
    public Node<T> To { get; set; }
    public int Weight {  get; set; }

    public Edge(Node<T> to, int weight)
    {
        To = to;
        Weight = weight;
    }
}



// 노드 클래스
public class Node<T>
{
    public T Value { get; set; } // 노드의 데이터
    public List<Edge<T>> Neighbors { get; private set; } = new List<Edge<T>>(); // 이웃 노드 리스트

    public Node(T value)
    {
        Value = value;
    }
}

// 그래프 클래스
public class SimpleGraph<T>
{
    private List<Node<T>> _nodes = new List<Node<T>>(); // 그래프의 모든 노드 저장

    // 노드 추가 메서드
    public Node<T> AddNode(T value)
    {
        var node = new Node<T>(value);
        _nodes.Add(node);
        return node;
    }

    // 단방향 간선 추가
    public void AddEdge(Node<T> from, Node<T> to, int weight)
    {
        from.Neighbors.Add(new Edge<T> (to, weight));
    }

    // 양방향 간선 추가
    public void AddUndirectedEdge(Node<T> a, Node<T> b, int weight)
    {
        a.Neighbors.Add(new Edge<T>(b,weight));//a>b갈때 weight분
        b.Neighbors.Add(new Edge<T>(a,weight));
    }

    // 그래프 출력
    public void PrintGraph()
    {
        foreach (var node in _nodes)
        {
            Console.Write($"{node.Value} -> ");
            foreach (var neighbor in node.Neighbors)
            {
                Console.Write($"{neighbor.To.Value}, 걸리는 시간 : {neighbor.Weight} ");
            }
            Console.WriteLine();
        }
    }

    // BFS 탐색
    public void Bfs(Node<T> start, Node<T> target)
    {
        Queue<Node<T>> queue = new Queue<Node<T>>();
        List<Node<T>> visited = new List<Node<T>>(); // 방문 기록을 List로 관리

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Node<T> current = queue.Dequeue();
            if (visited.Contains(current)) // 중복 방문 방지
                continue;

            Console.WriteLine($"노드 방문: {current.Value}");
            visited.Add(current); // 방문한 노드 기록

            if (current.Equals(target)) // 목표 노드 발견
            {
                Console.WriteLine($"목표 노드 {target.Value} 발견!");
                return;
            }

            foreach (var neighbor in current.Neighbors)
            {
                if (!visited.Contains(neighbor.To)) // 이미 방문한 노드는 제외
                    queue.Enqueue(neighbor.To);
            }
        }

        Console.WriteLine("경로를 찾을 수 없습니다.");
    }

    // DFS 탐색
    public void Dfs(Node<T> start, Node<T> target)
    {
        Stack<Node<T>> stack = new Stack<Node<T>>();
        List<Node<T>> visited = new List<Node<T>>(); // 방문 기록을 List로 관리

        stack.Push(start);

        while (stack.Count > 0)
        {
            Node<T> current = stack.Pop();
            if (visited.Contains(current)) // 중복 방문 방지
                continue;

            Console.WriteLine($"노드 방문: {current.Value}");
            visited.Add(current); // 방문한 노드 기록

            if (current.Equals(target)) // 목표 노드 발견
            {
                Console.WriteLine($"목표 노드 {target.Value} 발견!");
                return;
            }

            foreach (var neighbor in current.Neighbors)
            {
                if (!visited.Contains(neighbor.To)) // 이미 방문한 노드는 제외
                    stack.Push(neighbor.To  );
            }
        }

        Console.WriteLine("경로를 찾을 수 없습니다.");
    }
}

// 테스트 코드
class Program
{
    static void Main(string[] args)
    {
        //    SimpleGraph<string> graph = new SimpleGraph<string>();

        //    // 노드 추가
        //    var nodeA = graph.AddNode("A");
        //    var nodeB = graph.AddNode("B");
        //    var nodeC = graph.AddNode("C");
        //    var nodeD = graph.AddNode("D");
        //    var nodeE = graph.AddNode("E");

        //    // 간선 추가
        //    graph.AddEdge(nodeA, nodeB,5);
        //    graph.AddEdge(nodeA, nodeC,12);
        //    graph.AddEdge(nodeB, nodeD,7);
        //    graph.AddEdge(nodeC, nodeE,111);

        //    // 그래프 출력
        //    Console.WriteLine("그래프 구조:");
        //    graph.PrintGraph();

        //    // BFS 탐색
        //    Console.WriteLine("\nBFS 탐색:");
        //    graph.Bfs(nodeA, nodeE);

        //    // DFS 탐색
        //    Console.WriteLine("\nDFS 탐색:");
        //    graph.Dfs(nodeA, nodeE);

        //tempclass tempclass = new tempclass();// 그냥 객체 만듬
        //mydel mydel = null;//참조형, 주소를담음

        //뉴할당해서 델리게이트 형을 만들되, 매개변수로 메서드 넣어주기
        //mydel = new mydel(tempclass.PrintTemp);//인자x, 반환x인 애들만 기억 가능
        //mydel = tempclass.PrintTemp;
        //mydel = StaticMethod;//실체화 되어있는 주소가 필요하다
        //mydel = program.nonStaticMethod;//안됌,실체화 되어있는 주소가 필요하다

        //mydel = tempclass.PrintTemp;

        //int answer = mydel(4, 12);
        //Console.WriteLine(answer);

        ///////////////////////////////////////////////
        //델리게이트
        //delegate 체인 : 하나가 아닌 여러개
        // player player = new player();
        //myde2 change = UI.PlayerLevelUpAlert;
        //change += player.ChangPlayerHp;
        //change += player.ChangPlayerAtt;
        //change += player.ChangPlayerDef;

        //change(2);

        ////레벨업2
        //UI.PlayerLevelUpAlert(2);

        //player.ChangPlayerHp(2);
        //player.ChangPlayerDef(2);
        //player.ChangPlayerAtt(2);

        //Console.WriteLine("ddddddddddddddddddddddddd");

        ////레벨업 > 레벨3
        //UI.PlayerLevelUpAlert(1);
        //player.ChangPlayerHp(1);
        //player.ChangPlayerDef(1);
        //player.ChangPlayerAtt(1);


        ////레벨2업 
        //player player = new player();
        //player.LevelUp(2);
        ////////////////////////////////////////////////

        //람다식 
        //익명 함수 : 이름이 없는 함수 보통 짧은 코드의 델리게이트 대입 등에 사용
        //익명 함수 만드는 법은 아래와 같음
        //장점 : 코드 간결해짐
        //매서드로 뺴기 애매한,,, 그런거 람다식으로 하면 편함

        //   (입력 매개변수) => {실행 코드}
        //Calculator add;//델리게이트 하나 생성
        //add = (x, y) => x + y;
        //Calculator multiply = (x, y) => x * y;


        //Console.WriteLine($"5+3 = {add(5,3)}");
        //Console.WriteLine($"5*3 = {multiply(5,3)}");

        //////////////////////////
        //콜백 함수  (나중에 뭐가를 하겠다)
        //콜백 : 특정 작업이 완료된 후에 호출되는 메서드
        //예를 들어, 어떤 동작을 실행한 뒤, 그결과 바탕으로 다른 작업을 수행하고 싶을때

        //예시 : 유니티에서 버틀 클릭, 키입력(뭔가 처리를 하고 그에 따른 행동을 지시할때)
        //알고리즘 구현시, 동작을 유연하게 오름차순, 내림차순 바꿀때
        //+ 나중에 네트워크 같은 비동기 작업 때 콜백 도배 되어있음

        //HeavyWork(OnWorkComleted);
        //Console.WriteLine("메인 메서드 작업중임");

        //Console.WriteLine("타이머 시작");
        ////3초 후에 콜백 메서드 호출하는 기능
        //Timer timer = new Timer(Callback,null,3000,Timeout.Infinite);
        //Console.WriteLine("이게 먼저 실행될까?");
        //Console.ReadLine();//임시

        /////////////////////////////
        //진짜 유의미한 콜백
        //int[] numbers = { 5, 3, 8, 1, 2 };
        ////오름차순 정렬, 람다식
        //Sort(numbers, (a, b) => a < b);

        //Console.WriteLine("오름차순 : "+string.Join(",",numbers));
        ////내림차순 정렬, 람다식
        //Sort(numbers, (a, b) => a > b);

        //Console.WriteLine("내림차순 : " + string.Join(",", numbers));

        //Action. 일반적인 델리케이트

        //int a;
        //Action<string> b;//반환 없는 함수 담음

        //b= null;
        //b += Console.WriteLine;
        //b.Invoke("asd");

        ////////////////////////////////////////////////////
        //이벤트 : 델리게이트이긴 한데, 제약이 좀 더 걸린 델리게이트

        Button button = new Button();
        button.OnClick += () => Console.WriteLine("플레이어 점프");

        button.Click();








    }

    static void Sort(int[] array, ComparDelegate compare)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1; j++)
            {
                if (!compare(array[j], array[j + 1]))
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                }

            }

        }
    }
    static void Callback(object State)
    {
        Console.WriteLine("3초 후 실행될 메서드");
    }
    static void HeavyWork(CallbackDelegate callback)
    {
        Console.WriteLine("뭔가 시간 걸리는 게임 기능");
        Thread.Sleep(1000);//위작업 1초 걸렷다고 가정
        callback("작업이 완료되었습니다");

    }
    static void OnWorkComleted(string message)
    {
        Console.WriteLine($"콜백 호출 : {message}");
    }

    delegate int Calculator(int a, int b);//반환형은 int ,int값은 숫자 2개 받는 형식

    static void StaticMethod()
    {
        Console.WriteLine("정적 함수");
    }
    //실체를 가진 객체만 쓸수있음
    void nonStaticMethod()
    {
        Console.WriteLine("비정적 함수");
    }

    public delegate void CallbackDelegate(string message);
    public delegate bool ComparDelegate(int a, int b);

    public delegate void onclicked2();
    public onclicked2 Onclickdel;

    public Action Onclick; //액션으로 한번에// 반환값없고, 인자값 없는 델리게이트


    public delegate int CalcuclateDel(int x, int y);
    public CalcuclateDel calcuclate;

    public Func<int, int, int> calculateFunc;// 앞에 2개는 인자타입, 뒤에 마지막 하나는 리턴타입
}

class Button
{
    //이벤트는 외부에서 직접 시행(인보크) 할 수 없으며, 이벤트 정의한 클래스 내부에서만 호출가능
    //반면 이벤트 안붙인 델리게이트는 외부에서서 invoke 시킬 수 있음
    //다시 말해, 좀 더 안전한 델리게이트가 이벤트 델리게이트
    public event Action OnClick;//함수들을 담는 델리게이트 필드 선언
    
    public void Click()
    {
        OnClick += ButtonGreyOut;
        Console.WriteLine("버튼 클릭됨");
        OnClick?.Invoke();
    }
    public void ButtonGreyOut()
    {
        Console.WriteLine("버튼 회색처리됌");
    }
}


//델리게이트 설계도 만듬
//델리게이트 선언시, 반환과 인자값 형태를 이렇게 미리 적어줌
//위 설계도의 의미는 반환x, 인자x
//반환x, 인자x 함수들을 기억할 수 있는 형식인 거임
//delegate void mydel();
//모랄까 콘솔 같은 느낌임
delegate int StatChange(int a,int b);
delegate void myde2(int level);//반환없고 인자값은 있는 델리게이트설계도 만듬

class player
{
    int _hp = 100;
    int _att = 10;
    int _def = 30;
    myde2 statChangeAction;//스텟 변하면 실행될 메서드를 담는 델리게이트 객체

    public player()
    {
        statChangeAction += ChangPlayerHp;
        statChangeAction += ChangPlayerAtt;
        statChangeAction += ChangPlayerDef;
    }

    public void LevelUp(int level)
    {
        //이거 위험함
        //statChangeAction(level);
        //요거는 ㄱㅊ, inboke ?를 붙일수 있음, null생각 가능 //?.invoke
        //인보크는 델리게이트에 연결된 모든 기능을 수행하는 역할
        statChangeAction?.Invoke(level);

    }

    //레벨에 따른 스탯 변화
    public void ChangPlayerHp(int level)
    {
        _hp += level * 10;
        Console.WriteLine("플레이어 체력"+ _hp);

    }

    public void ChangPlayerAtt(int _att)
    {
        _hp += _att * 2;
        Console.WriteLine("플레이어 공격력" + _att);

    }

    public void ChangPlayerDef(int _def)
    {
        _hp += _def * 5;
        Console.WriteLine("플레이어 방어력" + _def);
    }

}

class UI
{
    static public void PlayerLevelUpAlert(int value)
    {
        Console.WriteLine($"플레이어 레벨이 {value}만큼 올랐습니다");
    }

    
}

class tempclass 
{
    public int PrintTemp(int toAdd, int toArr2)
    {
        return toAdd + toArr2;

    }

}


