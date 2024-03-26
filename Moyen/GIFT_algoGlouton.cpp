#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <math.h>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

 // Fonction pour échanger deux éléments dans le tableau
void swap(int& a, int& b) {
    int temp = a;
    a = b;
    b = temp;
}

// Fonction pour partitionner le tableau et retourner l'index du pivot
int partition(int arr[], int low, int high) {
    int pivot = arr[high]; // On prend le dernier élément comme pivot
    int i = low - 1; // Index du plus petit élément

    for (int j = low; j < high; ++j) {
        // Si l'élément actuel est plus petit ou égal au pivot, on l'échange avec l'élément à l'index i
        if (arr[j] <= pivot) {
            ++i;
            swap(arr[i], arr[j]);
        }
    }

    // Échanger l'élément suivant l'index i avec le pivot
    swap(arr[i + 1], arr[high]);
    return i + 1;
}

// Fonction récursive pour trier le tableau en utilisant le tri rapide
void quickSort(int arr[], int low, int high) {
    if (low < high) {
        int pivotIndex = partition(arr, low, high);

        // Trier les sous-tableaux avant et après le pivot
        quickSort(arr, low, pivotIndex - 1);
        quickSort(arr, pivotIndex + 1, high);
    }
}

int sum(int arr[], int low, int high)
{
    int res = 0;
    if (low < high) {
        for (int i = low; i < high; i++)
        {
            res += arr[i];
        }
    }
    return res;
}

int main()
{
    int n;
    cin >> n; cin.ignore();
    int c;
    cin >> c; cin.ignore();

    cerr << "c = " << c << endl;

    int tot[n];
    int res[n];
    int argent_totale = 0;

    int test = 0;

    for (int i = 0; i < n; i++) {
        int b;
        cin >> b; cin.ignore();
        tot[i] = (int)b;
        res[i] = 0;
        argent_totale += (int)b;
    }

    if (argent_totale < c)
    {
        cout << "IMPOSSIBLE" << endl;
    }
    else
    {
        int size = sizeof(tot) / sizeof(tot[0]);

        quickSort(tot, 0, size);

        int part = floor(c / n);

        part = floor(c / n);
        for (int u = 0; u < size; u++)
        {
            if (tot[u] > c / (n - u))
            {
                int res = floor(c / (n - u));
                cout << res << endl;
                tot[u] -= res;
                c -= res;
            }
            else
            {
                cout << tot[u] << endl;
                c -= tot[u];
                tot[u] = 0;
            }
        }
        cerr << c << endl;
    }
}