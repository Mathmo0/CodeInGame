#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <map>
#include <math.h>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    int w; // width of the building.
    int h; // height of the building.
    cin >> w >> h; cin.ignore();
    int n; // maximum number of turns before game over.
    cin >> n; cin.ignore();
    int x0;
    int y0;
    cin >> x0 >> y0; cin.ignore();

    std::map<string, int> map = { {"U", 1}, {"UR", 2}, {"R", 3}, {"DR", 4}, {"D", 5}, {"DL", 6}, {"L", 7}, {"UL", 8} };

    int min_w = 0;
    int min_h = 0;

    int max_w = w;
    int max_h = h;


    // game loop
    while (1) {
        string bomb_dir; // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
        cin >> bomb_dir; cin.ignore();

        int result_h = 0;
        int result_w = 0;

        int p = pow(1.9, n);
        double div = 2;
        int d = pow(1.9, n);

        switch (bomb_dir[0])
        {
        case 'U':
            result_h = (y0 + min_h - 1) / div;
            max_h = y0 - 1;
            break;
        case 'D':
            result_h = (max_h + y0 + 1) / div;
            min_h = y0 + 1;
            break;
        case 'L':
            result_w = (x0 + min_w - 1) / div;
            max_w = x0 - 1;
            break;
        case 'R':
            result_w = (max_w + x0 + 1) / div;
            min_w = x0 + 1;
            break;
        }

        if (bomb_dir.length() == 2)
        {
            switch (bomb_dir[1])
            {
            case 'R':
                result_w = (max_w + x0 + 1) / div;
                min_w = x0 + 1;
                break;
            case 'L':
                result_w = (x0 + min_w - 1) / div;
                max_w = x0 - 1;
                break;
            }
        }
        else
        {
            if (bomb_dir[0] == 'D' || bomb_dir[0] == 'U')
            {
                //result_w = max_w/div;
                result_w = x0;
            }
            else
            {
                //result_h = max_h/div;
                //max_h = y0 + 1;
                result_h = y0;
            }
        }

        x0 = result_w;
        y0 = result_h;

        n--;

        cerr << bomb_dir << endl;
        cerr << "max h" << max_h << endl;
        cerr << "max w" << max_w << endl;
        cerr << "min h" << min_h << endl;
        cerr << "min w" << min_w << endl;
        cerr << y0 << endl;
        cerr << div << endl;
        cerr << d << endl;

        // the location of the next window Batman should jump to.
        cout << result_w << ' ' << result_h << endl;
    }
}