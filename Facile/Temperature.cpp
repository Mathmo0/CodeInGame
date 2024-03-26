#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <limits>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    int n; // the number of temperatures to analyse
    cin >> n; cin.ignore();
    int min = numeric_limits<int>::max();;
    int res;
    for (int i = 0; i < n; i++) {
        int t; // a temperature expressed as an integer ranging from -273 to 5526
        cin >> t; cin.ignore();
        cerr << t << endl;
        if(abs(t) < min)
        {
            min = abs(t);
            res = t;
        }
        else if(abs(t) == min)
        {
            if(t > 0)
            {
                min = abs(t);
                res = t;
            }
        }
    }

    // Write an answer using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    cout << res << endl;
}