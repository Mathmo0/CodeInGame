#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <bitset>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

string test(int & i, string str)
{
    string r = "";
    switch(str[i])
    {
        case '1':
            r += "0 ";
            while(str[i] == '1')
            {
                r += "0";
                i++;
            }
            break;
        case '0':
            r += "00 ";
            while(str[i] == '0')
            {
                r += "0";
                i++;
            }
            break;
        default:
            break;
    }
    r += " ";
    return r;
}

int main()
{
    string message;
    getline(cin, message);

    string bit = "";
    string res = "";

    for(char c : message)
    {
        std::bitset<7> binary(c); 
        bit += binary.to_string(); 
    }

    int it = 0;

    while(it < bit.size())
    {
        res += test(it, bit);
        
    }



    // Write an answer using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    cout << res.substr(0, res.size() - 1) << endl;
}