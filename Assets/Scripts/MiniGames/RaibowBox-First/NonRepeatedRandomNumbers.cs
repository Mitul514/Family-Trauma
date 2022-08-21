using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NonRepeatedRandomNumbers : MonoBehaviour
{
    public int[] GetRandomNumber(int[] maxBoxesCount, int[] minesLength, int mineIndex = 0)
    {
        if (minesLength.Length == 0)
            return minesLength;

        var val = Random.Range(0, maxBoxesCount.Length);

        maxBoxesCount = SwapValWithLastIndexVal(maxBoxesCount, minesLength, val);

        if (mineIndex < minesLength.Length)
        {
            minesLength[mineIndex] = val;
            mineIndex++;
            GetRandomNumber(maxBoxesCount, minesLength, mineIndex);
        }

        return minesLength;
    }

    private int[] SwapValWithLastIndexVal(int[] boxArr, int[] minesArr, int val)
    {
        int temp = 0;

        temp = boxArr[val];
        boxArr[val] = boxArr[boxArr.Length - 1];
        boxArr[boxArr.Length - 1] = temp;

        boxArr = boxArr.SkipLast(1).ToArray();

        return boxArr;
    }
}
