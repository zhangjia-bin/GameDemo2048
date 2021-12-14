using System;
using System.Collections.Generic;

public class Core
{
    private int[,] mMap;
    //计算得分的计数器
    private int Socre = 0;

    public int GetSocre()
    {
        return Socre;
    }
    public Core(int row, int col)
    {
        //初始化地图数组
        mMap = new int[row, col];
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                mMap[i, j] = 0;
            }
        }
        CreateNumber();
        CreateNumber();
    }
    public void Print()
    {
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                Console.Write(mMap[i, j]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
    public bool Move(Director dir)
    {
        switch (dir)
        {
            case Director.UP:
                return MoveUp();
            case Director.DOWN:
                return MoveDown();
            case Director.LEFT:
                return MoveLeft();
            case Director.RIGHT:
               return MoveRight();
        }
       
        return true;
    }

    //合并一维数组的数据
    private bool Combine(int[] arr)
    {
        bool isCombine = false;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i] != 0 && arr[i] == arr[i + 1])
            {
                AdiouManager.Instance.PlayClipAudiou2();;
                arr[i] = arr[i] * 2;
                arr[i + 1] = 0;
                Socre++;
                isCombine = true;
            }
        }
       
        return isCombine;
    }
    //移动清除Function
    //
    private bool ClearZero(int[] arr)
    {
        int index = 0;
        bool isMove = false;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != 0)
            {
                if (index != i)
                {
                    arr[index] = arr[i];
                    arr[i] = 0;
                    isMove = true;
                }
                index++;
            }
        }
        return isMove;
    }
    //提供一个获取数值的代码
    public int GetValue(int raw,int col)
    {
        return mMap[raw, col];
    }

    private bool MoveLeft()
    {
        bool isChanger = false;
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            int[] tempArr = new int[mMap.GetLength(1)];
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                tempArr[j] = mMap[i, j];
            }
            //1，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //2，合并
            if (Combine(tempArr))
            {
                isChanger = true;
            }
            //3，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //4，把一维数组的值赋给我们的二维数组
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                mMap[i, j] = tempArr[j];
            }
        }
        if (isChanger)
        {
            CreateNumber();
        }
        return IsGameOver();
    }
    private bool MoveRight()
    {
        bool isChanger = false;
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            int[] tempArr = new int[mMap.GetLength(1)];
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                tempArr[j] = mMap[i, mMap.GetLength(1) - 1 - j];
            }
            //1，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //2，合并
            if (Combine(tempArr))
            {
                isChanger = true;
            }
            //3，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //4，把一维数组的值赋给我们的二维数组
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                mMap[i, mMap.GetLength(1) - 1 - j] = tempArr[j];
            }
        }
        if (isChanger)
        {
            CreateNumber();
        }
        return IsGameOver();
    }
    private bool MoveDown()
    {
        bool isChanger = false;
        for (int i = 0; i < mMap.GetLength(1); i++)
        {
            int[] tempArr = new int[mMap.GetLength(0)];
            for (int j = 0; j < mMap.GetLength(0); j++)
            {
                tempArr[j] = mMap[mMap.GetLength(0) - 1 - j, i];
            }
            //1，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //2，合并
            if (Combine(tempArr))
            {
                isChanger = true;
            }
            //3，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //4，把一维数组的值赋给我们的二维数组
            for (int j = 0; j < mMap.GetLength(0); j++)
            {
                mMap[mMap.GetLength(0) - 1 - j, i] = tempArr[j];
            }
        }
        if (isChanger)
        {
            CreateNumber();
        }
        return IsGameOver();
    }
    private bool MoveUp()
    {
        bool isChanger = false;
        for (int i = 0; i < mMap.GetLength(1); i++)
        {
            int[] tempArr = new int[mMap.GetLength(0)];
            for (int j = 0; j < mMap.GetLength(0); j++)
            {
                tempArr[j] = mMap[j, i];
            }
            //1，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //2，合并
            if (Combine(tempArr))
            {
                isChanger = true;
            }
            //3，移动
            if (ClearZero(tempArr))
            {
                isChanger = true;
            }
            //4，把一维数组的值赋给我们的二维数组
            for (int j = 0; j < mMap.GetLength(0); j++)
            {
                mMap[j, i] = tempArr[j];
            }
        }

        if (isChanger)
        {
            CreateNumber();
        }
        return IsGameOver();
    }

    //随机创建一个数字
    void CreateNumber()
    {
        List<int> emptyList = new List<int>();
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                if (mMap[i, j] == 0)
                {
                    emptyList.Add(i * mMap.GetLength(1) + j);
                }
            }
        }
        Random random = new Random();
        int index = random.Next(emptyList.Count);
        int raw = emptyList[index] / mMap.GetLength(1);
        int col = emptyList[index] % mMap.GetLength(1);

        mMap[raw, col] = 2;
    }

    //赢取比赛
    public bool GetWin()
    {
        bool flag = false;
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                if (mMap[i,j]==1024)
                {
                    flag = true;
                    return flag;
                }
               
            }
        }

        return flag;
    }

    //判断游戏结束的方法
    public bool IsGameOver()
    {
        for (int i = 0; i < mMap.GetLength(0); i++)
        {
            for (int j = 0; j < mMap.GetLength(1); j++)
            {
                //1.当前的位置为0
                if (mMap[i,j]==0)
                {
                    return false;
                }
                //2.当前位置得数和上面的一样
                if (i>0&&mMap[i,j]==mMap[i-1,j])
                {
                    return false;
                }
                //3.当前位置得数和下面的一样
                if (i<mMap.GetLength(0)-1&&mMap[i,j]==mMap[i+1,j])
                {
                    return false;
                }
                //4.当前位置得数和左边的一样
                if (j>0&&mMap[i,j]==mMap[i,j-1])
                {
                    return false;
                }
                //5.当前位置得数和右边的一样
                if (j<mMap.GetLength(1)-1&&mMap[i,j]==mMap[i,j+1])
                {
                    return false;
                }
            }
        }

        return true;
    }


}
