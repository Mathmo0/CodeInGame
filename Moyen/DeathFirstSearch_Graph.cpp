#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    int n; // the total number of nodes in the level, including the gateways
    int l; // the number of links
    int e; // the number of exit gateways
    cin >> n >> l >> e; cin.ignore();

    int end[e];
    int linkA[l];
    int linkB[l];

    // struc qui fait ref aux sortie avec leur arcs
    struct star {
        int num;
        vector<vector<int>> link;
    };

    vector<star> stars;
    vector<vector<int>> otherLink;

    cerr << "l = " << l << endl;

    for (int i = 0; i < l; i++) {
        int n1; // N1 and N2 defines a link between these nodes
        int n2;
        cin >> n1 >> n2; cin.ignore();
        linkA[i] = n1;
        linkB[i] = n2;
    }
    for (int i = 0; i < e; i++) {
        int ei; // the index of a gateway node
        cin >> ei; cin.ignore();
        cerr << ei << endl;
        end[i] = ei;
        star s = star();
        s.num = ei;

        for (int t = 0; t < l; t++)
        {
            if (linkA[t] == ei || linkB[t] == ei)
            {
                vector<int> link{linkA[t], linkB[t]};
                s.link.push_back(link);
            }
            else
            {
                vector<int> linkBis{linkA[t], linkB[t]};
                otherLink.push_back(linkBis);
            }
        }
        stars.push_back(s);
    }

    // game loop
    while (1) {
        int breakA = 0;
        int breakB = 0;

        int si; // The index of the node on which the Bobnet agent is positioned this turn
        cin >> si; cin.ignore();

        // Write an action using cout. DON'T FORGET THE "<< endl"
        // To debug: cerr << "Debug messages..." << endl;
        for (int z = 0; z < e; z++)
        {

        }

        int stop = false;
        for (int y = 0; y < e; y++)
        {
            cerr << si << endl;
            //breakA = stars[y].link.back()[0];
            //breakB = stars[y].link.back()[1];

            // On parcous la liste des sorties pour voir si le mechant s'apprete à rentrer
            for (int u = 0; u < stars[y].link.size(); u++)
            {
                if (stars[y].link[u][0] == si || stars[y].link[u][1] == si)
                {
                    cerr << "here " << y << endl;
                    breakA = stars[y].link[u][0];
                    breakB = stars[y].link[u][1];
                    stars[y].link.erase(stars[y].link.begin() + u);
                    stop = true;
                    break;
                }
            }
        }

        if (!stop)
        {
            // On parcours les autres link non lié aux sorties pour les couper aussi si besoin
            for (int p = 0; p < otherLink.size(); p++)
            {
                if (otherLink[p][0] == si || otherLink[p][1] == si)
                {
                    breakA = otherLink[p][0];
                    breakB = otherLink[p][1];
                    otherLink.erase(otherLink.begin() + p);
                    break;
                }
            }
            //breakA = stars.back().link.back()[0];
            //breakB = stars.back().link.back()[1];
            //stars.back().link.pop_back();
        }

        cerr << breakA << " " << breakB << endl;
        // Example: 0 1 are the indices of the nodes you wish to sever the link between
        cout << breakA << ' ' << breakB << endl;
    }
}