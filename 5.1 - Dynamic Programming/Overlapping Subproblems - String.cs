﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DataStructuresAndAlgorithms
{
    /// String Problems

    /*
    Input : Str1: I Am Good.
            Str2: You Are Good.

    Output:  Longest Common Sub Sequence : AGood.
    
    http://en.wikipedia.org/wiki/Longest_common_subsequence_problem
    http://www.algorithmist.com/index.php/Longest_Common_Subsequence
    https://www.youtube.com/watch?v=P-mMvhfJhu8
    https://www.youtube.com/watch?v=IFQfU5pQ8-I
    http://www.geeksforgeeks.org/longest-common-subsequence/
    http://www.geeksforgeeks.org/dynamic-programming-set-4-longest-common-subsequence/
    http://www.geeksforgeeks.org/dynamic-programming-set-12-longest-palindromic-subsequence/
    http://www.geeksforgeeks.org/manachers-algorithm-linear-time-longest-palindromic-substring-part-2/
    https://github.com/mission-peace/interview/blob/master/src/com/interview/dynamic/LongestPalindromicSubsequence.java
    https://www.hackerearth.com/practice/algorithms/string-algorithm/manachars-algorithm/tutorial/
    */
    partial class DynamicProgrammingSamples
    {
        // Time Complexity O(N X M) Returns length of LCS for X[0..m-1], Y[0..n-1]
        int LongestCommonSubSequenceDP(string s1, string s2)
        {
            int[,] lkUpMtrx = new int[s1.Length + 1, s2.Length + 1];

            // Following steps build L[m+1,n+1] in bottom up fashion. 
            for (int s1in = 1; s1in <= s1.Length; s1in++)
            {
                for (int s2in = 1; s2in <= s2.Length; s2in++)
                {
                    if (s1[s1in - 1] == s2[s2in - 1])
                    {
                        lkUpMtrx[s1in, s2in] = lkUpMtrx[s1in - 1, s2in - 1] + 1;
                    }
                    else
                    {
                        lkUpMtrx[s1in, s2in] = Math.Max(lkUpMtrx[s1in - 1, s2in],
                                                        lkUpMtrx[s1in, s2in - 1]);
                    }
                }
            }

            // Contains length of LCS for inputStr1[0..n-1] and inputStr2[0..m-1]
            return lkUpMtrx[s1.Length, s2.Length];
        }

        // Time Complexity O(2^N) Exponential Time. Returns length of LCS for X[0..m-1], Y[0..n-1]

        int LongestCommonSubSequenceRec(string str1, string str2, int str1Indx, int str2Indx)
        {
            if (str1Indx == 0 || str2Indx == 0)
                return 0;

            if (str1[str1Indx - 1] == str2[str2Indx - 1])
                return 1 + LongestCommonSubSequenceRec(str1, str2, str1Indx - 1, str2Indx - 1);

            return Math.Max(LongestCommonSubSequenceRec(str1, str2, str1Indx, str2Indx - 1),
                            LongestCommonSubSequenceRec(str1, str2, str1Indx - 1, str2Indx));
        }

        public void LongestCommonSubSequenceDPTest()
        {            
            string str1 = "IAmGood";
            string str2 = "UAreGood"; // AGood is the CSS.

            //string str1 = "ABCDGH";
            //string str2 = "AEDFHR";

            //string str1 = "AGGTAB";
            //string str2 = "GXTXAYB";

            //MessageBox.Show("Length of LCS is \n" + LongestCommonSubSequenceRec(str1, str2, str1.Length, str2.Length));
            MessageBox.Show("Length of LCS is \n" + LongestCommonSubSequenceDP(str1, str2));
        }

        // https://leetcode.com/problems/longest-uncommon-subsequence-ii/description/
        int LongestUnCommonSubSequenceDP(string str1, string str2)
        {
            int[,] lkUpMtrx = new int[str1.Length + 1, str2.Length + 1];

            // Following steps build L[m+1,n+1] in bottom up fashion. 
            for (int str1Indx = 0; str1Indx <= str1.Length; str1Indx++)
            {
                for (int str2Indx = 0; str2Indx <= str2.Length; str2Indx++)
                {
                    if (str1Indx == 0 || str2Indx == 0)
                    {
                        lkUpMtrx[str1Indx, str2Indx] = 0;
                    }
                    else if (str1[str1Indx - 1] == str2[str2Indx - 1])
                    {
                        lkUpMtrx[str1Indx, str2Indx] = lkUpMtrx[str1Indx - 1, str2Indx - 1] + 1;
                    }
                    else
                    {
                        lkUpMtrx[str1Indx, str2Indx] = Math.Max(lkUpMtrx[str1Indx - 1, str2Indx],
                                                                    lkUpMtrx[str1Indx, str2Indx - 1]);
                    }
                }
            }

            // Contains length of LCS for inputStr1[0..n-1] and inputStr2[0..m-1]
            return lkUpMtrx[str1.Length, str2.Length];
        }

        public int LongestPalindromeSubseq(string str)
        {
            int strLen = str.Length;
            if (strLen <= 1)
            {
                return strLen;
            }

            /*dp[i][j] means from index i to index j, the longest palindromic subsequence*/
            int[,] lkUpMat = new int [strLen + 1, strLen + 1];

            /*dp initialization dp[i,i] means contains one character so it equals one*/
            for (int indx = 1; indx <= strLen; ++indx)
            {
                lkUpMat[indx , indx] = 1;
            }

            for (int lIndx = 1; lIndx < strLen; ++lIndx)
            {
                for (int rIndx = lIndx - 1; rIndx >= 0; --rIndx)
                {
                    if (str[rIndx] == str[lIndx])
                    {
                        lkUpMat[rIndx,lIndx] = Math.Max(lkUpMat[rIndx + 1,lIndx - 1] + 2, 
                                            Math.Max(lkUpMat[rIndx + 1,lIndx], lkUpMat[rIndx,lIndx - 1]));
                    }
                    else
                    {
                        lkUpMat[rIndx,lIndx] = Math.Max(lkUpMat[rIndx + 1,lIndx - 1],
                                        Math.Max(lkUpMat[rIndx + 1,lIndx], lkUpMat[rIndx,lIndx - 1]));
                    }
                }
            }
            return lkUpMat[0 ,strLen - 1];
        }

        public int LongestPalindromeSubseq2(String str)
        {
            int[,] lkUp = new int[str.Length, str.Length];

            for (int lIndx = str.Length - 1; lIndx >= 0; lIndx--)
            {
                lkUp[lIndx, lIndx] = 1;

                for (int rIndx = lIndx + 1; rIndx < str.Length; rIndx++)
                {
                    if (str[lIndx] == str[rIndx])
                    {
                        lkUp[lIndx, rIndx] = lkUp[lIndx + 1, rIndx - 1] + 2;
                    }
                    else
                    {
                        lkUp[lIndx, rIndx] = Math.Max(lkUp[lIndx + 1, rIndx], 
                                                      lkUp[lIndx, rIndx - 1]);
                    }
                }
            }

            return lkUp[0, str.Length - 1];
        }

        /*
        http://www.dsalgo.com/2013/02/longest-common-subsequence.html
        http://www.geeksforgeeks.org/longest-common-substring/

        */
        public int LongestCommonSubStringDP(string str1, string str2)
        {
            //Note that LCSuff[i,j] contains length of longest common suffix of X[0..i-1] and Y[0..j-1].
            //The first row and first column entries have no logical meaning, they are used only for simplicity of program

            int[,] lkUp = new int[str1.Length + 1, str2.Length + 1];
            int result = 0;

            // Following steps build lkUp[m+1,n+1] in bottom up fashion
            for (int str1Indx = 0; str1Indx <= str1.Length; str1Indx++)
            {
                for (int str2Indx = 0; str2Indx <= str2.Length; str2Indx++)
                {
                    if (str1Indx == 0 || str2Indx == 0)
                    {
                        lkUp[str1Indx, str2Indx] = 0;
                    }
                    else if (str1[str1Indx - 1] == str2[str2Indx - 1])
                    {
                        lkUp[str1Indx, str2Indx] = lkUp[str1Indx - 1, str2Indx - 1] + 1;
                        result = Math.Max(result, lkUp[str1Indx, str2Indx]);
                    }
                    else
                    {
                        lkUp[str1Indx, str2Indx] = 0;
                    }
                }
            }

            return result;
        }

        // Driver Program to test above function
        public void LongestCommonSubStringDPTest(String[] args)
        {
            String str1 = "OldSite:GeeksforGeeks.org";
            String str2 = "NewSite:GeeksQuiz.com";

            MessageBox.Show("Length of Longest Common Substring is " + LongestCommonSubStringDP(str1, str2));
        }

        /*
===================================================================================================================================================================================================

// Refer IsPalindrome program in String algorithms first.

http://en.wikipedia.org/wiki/Longest_palindromic_substring
http://www.careercup.com/question?id=4128790
http://www.careercup.com/question?id=15074748

http://www.dsalgo.com/2013/02/longest-palindrome-dynamic.htmls
http://www.geeksforgeeks.org/longest-palindrome-substring-set-1/
http://stackoverflow.com/questions/7043778/longest-palindrome-in-a-string-using-suffix-tree
N^2 Lil Easy   http://www.sourcetricks.com/2012/07/find-longest-palindrome-in-string.html


1.The left side of a palindrome is a mirror image of its right side.

2.(Case 1) A third palindrome whose center is within the right side of a first palindrome will have exactly the same length as that of a second palindrome anchored at the mirror center on the left side, 
if the second palindrome is within the bounds of the first palindrome by at least one character.

3.(Case 2) If the second palindrome meets or extends beyond the left bound of the first palindrome, 
then the third palindrome is guaranteed to have at least the length from its own center to the right outermost character of the first palindrome. 
This length is the same from the center of the second palindrome to the left outermost character of the first palindrome.

4.To find the length of the third palindrome under Case 2, the next character after the right outermost character of the first palindrome would then be compared with its mirror character around the center of the third palindrome, 
until there is no match or no more characters to compare.

5.(Case 3) Neither the first nor second palindrome provides information to help determine the palindromic length of a fourth palindrome whose center is outside the right side of the first palindrome.

6.It is therefore desirable to have a palindrome as a reference (i.e., the role of the first palindrome) that possesses characters furtherest to the right in a string 
when determining from left to right the palindromic length of a substring in the string (and consequently, 
the third palindrome in Case 2 and the fourth palindrome in Case 3 could replace the first palindrome to become the new reference).

7.Regarding the time complexity of palindromic length determination for each character in a string: 
there is no character comparison for Case 1, while for Cases 2 and 3 only the characters in the string beyond the right outermost character of the reference palindrome are candidates for comparison 
(and consequently Case 3 always results in a new reference palindrome while Case 2 does so only if the third palindrome is actually longer than its guaranteed minimum length).

8.For even-length palindromes, the center is at the boundary of the two characters in the middle.

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Given a string S, we are to find the longest sub-string s of S such that the reverse of s is exactly the same as s.
First insert a special character ‘#’ between each pair of adjacent characters of S, in front of S and at the back of S. After that, we only need to check palindrome sub-strings of odd length.
Let P[i] be the largest integer d such that S[i-d,...,i+d] is a palindrome.  We calculate all P[i]s from left to right. When calculating P[i], we have to compare S[i+1] with S[i-1], S[i+2] with S[i-2] and so on. A comparison is successful if two characters are the same, otherwise it is unsuccessful. In fact, we can possibly skip some unnecessary comparisons utilizing the previously calculated P[i]s.
Assume P[a]+a=max{ P[j]+j :  j<i }. If P[a]+a >= i, then we have 
P[i] >= min{ P[2*a-i],  2*a-i-(a- P[a])}.
Is it the algorithm linear time? The answer is yes.
First the overall number of unsuccessful comparisons is obviously at most N.
A more careful analysis show that S[i] would never be compared successfully with any S[j](j<i) after its first time successful comparison with some S[k] (k<i).
So the number of overall comparisons is a most 2N. 

===================================================================================================================================================================================================    
*/
        public void LargestPalindromeManacherAlgorithmTest()
        {
            //string strPalinSource = "ababccbcdddcecdddcbccbaba";
            //string strPalinResult = string.Empty;

            string resultString = string.Empty;

            string strPalinSource3 = "123321";
            string strPalinResult3 = LargestPalindromeManacherAlgorithm(strPalinSource3);
            resultString += ("\n Test 1 :  " + strPalinSource3 + " Result : " + strPalinResult3);

            string strPalinSource4 = "1234321";
            string strPalinResult4 = LargestPalindromeManacherAlgorithm(strPalinSource4);
            resultString += ("\n Test 2 :  " + strPalinSource4 + " Result : " + strPalinResult4);

            string strPalinSource5 = "000";
            string strPalinResult5 = LargestPalindromeManacherAlgorithm(strPalinSource5);
            resultString += ("\n Test 3 :  " + strPalinSource5 + " Result : " + strPalinResult5);


            string strPalinSource2 = "123321456";
            string strPalinResult2 = LargestPalindromeManacherAlgorithm(strPalinSource2);
            resultString += ("\n Test 4 :  " + strPalinSource2 + " Result : " + strPalinResult2);

            String strPalinSource1 = "456789123123321456789123";
            string strPalinResult1 = LargestPalindromeManacherAlgorithm(strPalinSource1);
            resultString += "\n Test 5 :  " + strPalinSource1 + " Result : " + strPalinResult1;

            MessageBox.Show(resultString);
        }

        public String LargestPalindromeManacherAlgorithm(String srcStr)
        {
            if (srcStr == null || srcStr.Length == 0)
            {
                return string.Empty;
            }

            //O(n) Extra Space
            int[] processedIndeces = new int[srcStr.Length];

            int c = 0;
            int r = 0; // Here the first element in s2 has been processed.

            int rIndx = 0;
            int lIndx = 0; // The walking indices to compare if two elements are the same

            for (int lpCnt = 1; lpCnt < srcStr.Length; lpCnt++)
            {
                if (lpCnt > r)
                {
                    processedIndeces[lpCnt] = 0;
                    rIndx = lpCnt - 1;
                    lIndx = lpCnt + 1;
                }
                else
                {
                    int i2 = c * 2 - lpCnt;

                    if (processedIndeces[i2] < (r - lpCnt))
                    {
                        processedIndeces[lpCnt] = processedIndeces[i2];
                        rIndx = -1; // This signals bypassing the while loop below. 
                    }
                    else
                    {
                        processedIndeces[lpCnt] = r - lpCnt;
                        lIndx = r + 1;
                        rIndx = lpCnt * 2 - lIndx;
                    }
                }

                while (rIndx >= 0 && lIndx < srcStr.Length &&
                        srcStr[rIndx] == srcStr[lIndx])
                {
                    processedIndeces[lpCnt]++;
                    rIndx--;
                    lIndx++;
                }

                if ((lpCnt + processedIndeces[lpCnt]) > r)
                {
                    c = lpCnt;
                    r = lpCnt + processedIndeces[lpCnt];
                }
            }

            int len = 0;

            c = 0;

            for (int lpCnt = 1; lpCnt < srcStr.Length; lpCnt++)
            {
                if (len < processedIndeces[lpCnt])
                {
                    len = processedIndeces[lpCnt];
                    c = lpCnt;
                }
            }

            char[] processedCharArray = new char[srcStr.Length];
            Array.Copy(processedIndeces, c - len, processedCharArray, 0, c + len + 1);

            return new string(RemoveBoundariesPostProcessing(processedCharArray));
        }

        //O(n) time
        private char[] AddBoundariesPreProcessing(char[] sourceCharArray)
        {
            if (sourceCharArray == null || sourceCharArray.Length == 0)
            {
                return "||".ToCharArray();
            }

            char[] processedCharArray = new char[sourceCharArray.Length * 2 + 1];

            for (int lpCnt = 0; lpCnt < (processedCharArray.Length - 1); lpCnt = lpCnt + 2)
            {
                processedCharArray[lpCnt] = '|';
                processedCharArray[lpCnt + 1] = sourceCharArray[lpCnt / 2];
            }

            processedCharArray[processedCharArray.Length - 1] = '|';

            return processedCharArray;
        }

        //O(n) time
        private char[] RemoveBoundariesPostProcessing(char[] sourceCharArray)
        {
            if (sourceCharArray == null || sourceCharArray.Length < 3)
            {
                return "".ToCharArray();
            }

            char[] filteredCharArray = new char[(sourceCharArray.Length - 1) / 2];

            for (int lpCnt = 0; lpCnt < filteredCharArray.Length; lpCnt++)
            {
                filteredCharArray[lpCnt] = sourceCharArray[lpCnt * 2 + 1];
            }
            return filteredCharArray;
        }

        // 680 https://leetcode.com/problems/valid-palindrome-ii/description/
        public bool ValidPalindromeII(string s)
        {
            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                if (s[left] == s[right])
                {
                    left++;
                    right--;
                }
                else
                {
                    //remove right
                    int templeft = left;
                    int tempright = right - 1;

                    while (templeft < tempright)
                    {
                        if (s[templeft] != s[tempright])
                            break;

                        templeft++;
                        tempright--;

                        if (templeft >= tempright)
                            return true;
                    }

                    //remove left
                    left++;

                    while (left < right)
                    {
                        if (s[left] != s[right])
                            return false;
                        left++;
                        right--;
                    }
                }
            }
            return true;
        }

        public bool ValidPalindromeIIRec(String srcStr)
        {
            int lIndx = 0;
            int rIndx = srcStr.Length - 1;

            while (lIndx < rIndx)
            {
                if (srcStr[lIndx] != srcStr[rIndx])
                {
                    return ValidPalindromeIIRecHlpr(srcStr, lIndx + 1, rIndx) ||
                            ValidPalindromeIIRecHlpr(srcStr, lIndx, rIndx - 1);
                }

                ++lIndx;
                --rIndx;
            }
            return true;
        }

        public bool ValidPalindromeIIRecHlpr(String srcStr, int lIndx, int rIndx)
        {
            while (lIndx < rIndx)
            {
                if (srcStr[lIndx] == srcStr[rIndx])
                {
                    ++lIndx;
                    --rIndx;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        //===========================================================================================

        //303 https://leetcode.com/problems/range-sum-query-immutable/description/
        /*
            Given an integer array nums, find the sum of the elements between indices i and j (i ≤ j), inclusive.
            Example:
            Given nums = [-2, 0, 3, -5, 2, -1]

            sumRange(0, 2) -> 1
            sumRange(2, 5) -> -1
            sumRange(0, 5) -> -3

            Note:
            You may assume that the array does not change.
            There are many calls to sumRange function.
                 * */
        public class NumArray
        {
            int[] nums;

            public NumArray(int[] nums)
            {
                for (int indx = 1; indx < nums.Length; indx++)
                {
                    nums[indx] += nums[indx - 1];
                }
                this.nums = nums;
            }

            public int SumRange(int stIndx, int edIndx)
            {
                if (stIndx == 0)
                {
                    return nums[edIndx];
                }
                return nums[edIndx] - nums[stIndx - 1];
            }
        }

        // 304 https://leetcode.com/problems/range-sum-query-2d-immutable/
        /*
        Time complexity construction O(n*m)
        Time complexity of query O(1)
        Space complexity is O(n*m)
        */

        public class Immutable2DSumRangeQuery
        {
            private int[,] lkUpMat;

            public Immutable2DSumRangeQuery(int[,] srcMatrix)
            {

                lkUpMat = new int[srcMatrix.GetLength(0) + 1, srcMatrix.GetLength(1) + 1];

                for (int rIndx = 1; rIndx < lkUpMat.GetLength(0); rIndx++)
                {
                    for (int cIndx = 1; cIndx < lkUpMat.GetLength(1); cIndx++)
                    {
                        lkUpMat[rIndx, cIndx] = lkUpMat[rIndx - 1, cIndx] + lkUpMat[rIndx, cIndx - 1] +
                                                srcMatrix[rIndx - 1, cIndx - 1] - lkUpMat[rIndx - 1, cIndx - 1];
                    }
                }
            }

            public int SumQuery(int r1, int c1, int r2, int c2)
            {
                r1++;
                c1++;
                r2++;
                c2++;

                return lkUpMat[r2, c2] - lkUpMat[r1 - 1, c2] - lkUpMat[r2, c1 - 1]
                     + lkUpMat[r1 - 1, c1 - 1];
            }

            public static void Immutable2DSumRangeQueryTest()
            {
                int[,] input = {{3, 0, 1, 4, 2},
                        {5, 6, 3, 2, 1},
                        {1, 2, 0, 1, 5},
                        {4, 1, 0, 1, 7},
                        {1, 0, 3, 0, 5}};

                int[,] input1 = { { 2, 0, -3, 4 }, { 6, 3, 2, -1 }, { 5, 4, 7, 3 }, { 2, -6, 8, 1 } };
                Immutable2DSumRangeQuery isr = new Immutable2DSumRangeQuery(input1);
                Console.WriteLine(isr.SumQuery(1, 1, 2, 2));
            }
        }

        //===========================================================================================

        //303 https://leetcode.com/problems/range-sum-query-immutable/description/
        /*
            Given an integer array nums, find the sum of the elements between indices i and j (i ≤ j), inclusive.
            Example:
            Given nums = [-2, 0, 3, -5, 2, -1]

            sumRange(0, 2) -> 1
            sumRange(2, 5) -> -1
            sumRange(0, 5) -> -3

            Note:
            You may assume that the array does not change.
            There are many calls to sumRange function.
                 * */
        public class NumArray1
        {
            int[] nums;

            public NumArray1(int[] nums)
            {
                for (int indx = 1; indx < nums.Length; indx++)
                {
                    nums[indx] += nums[indx - 1];
                }
                this.nums = nums;
            }

            public int SumRange(int stIndx, int edIndx)
            {
                if (stIndx == 0)
                {
                    return nums[edIndx];
                }
                return nums[edIndx] - nums[stIndx - 1];
            }
        }

        // 304 https://leetcode.com/problems/range-sum-query-2d-immutable/
        /*
        Time complexity construction O(n*m)
        Time complexity of query O(1)
        Space complexity is O(n*m)
        */

        public class Immutable2DSumRangeQuery1
        {
            private int[,] lkUpMat;

            public Immutable2DSumRangeQuery1(int[,] srcMatrix)
            {

                lkUpMat = new int[srcMatrix.GetLength(0) + 1, srcMatrix.GetLength(1) + 1];

                for (int rIndx = 1; rIndx < lkUpMat.GetLength(0); rIndx++)
                {
                    for (int cIndx = 1; cIndx < lkUpMat.GetLength(1); cIndx++)
                    {
                        lkUpMat[rIndx, cIndx] = lkUpMat[rIndx - 1, cIndx] + lkUpMat[rIndx, cIndx - 1] +
                                                srcMatrix[rIndx - 1, cIndx - 1] - lkUpMat[rIndx - 1, cIndx - 1];
                    }
                }
            }

            public int SumQuery(int r1, int c1, int r2, int c2)
            {
                r1++;
                c1++;
                r2++;
                c2++;

                return lkUpMat[r2, c2] - lkUpMat[r1 - 1, c2] - lkUpMat[r2, c1 - 1]
                     + lkUpMat[r1 - 1, c1 - 1];
            }

            public static void Immutable2DSumRangeQueryTest()
            {
                int[,] input = {{3, 0, 1, 4, 2},
                        {5, 6, 3, 2, 1},
                        {1, 2, 0, 1, 5},
                        {4, 1, 0, 1, 7},
                        {1, 0, 3, 0, 5}};

                int[,] input1 = { { 2, 0, -3, 4 }, { 6, 3, 2, -1 }, { 5, 4, 7, 3 }, { 2, -6, 8, 1 } };
                Immutable2DSumRangeQuery isr = new Immutable2DSumRangeQuery(input1);
                Console.WriteLine(isr.SumQuery(1, 1, 2, 2));
            }
        }
    }
}