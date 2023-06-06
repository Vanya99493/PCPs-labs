#include <iostream>
#include <cstdlib>
#include <tuple>
#include"omp.h"

using namespace std;

const int ROWS = 1000;
const int COLUMNS = 1000;

int myArray[ROWS][COLUMNS];
long long minSumRowValue;;

void fill_my_array();
long long get_sum_array(int);
int get_min_row_index(int);

int main() {
    fill_my_array();

    omp_set_nested(1);

    double timeBefore = omp_get_wtime();
#pragma omp parallel sections
    {
#pragma omp section
        {
            cout << "1 thread sum = " << get_sum_array(1) << endl;
            cout << "2 threads sum = " << get_sum_array(2) << endl;
            cout << "3 threads sum = " << get_sum_array(3) << endl;
            cout << "4 threads sum = " << get_sum_array(4) << endl;
            /*cout << "sum 8 = " << get_sum_array(8) << endl;
            cout << "sum 10 = " << get_sum_array(10) << endl;
            cout << "sum 16 = " << get_sum_array(16) << endl;
            cout << "sum 32 = " << get_sum_array(32) << endl;*/
        }
#pragma omp section
        {
            cout << "1 thread min = " << minSumRowValue << "; index = " << get_min_row_index(1) << endl;
            cout << "2 threads min = " << minSumRowValue << "; index = " << get_min_row_index(2) << endl;
            cout << "3 threads min = " << minSumRowValue << "; index = " << get_min_row_index(3) << endl;
            cout << "4 threads min = " << minSumRowValue << "; index = " << get_min_row_index(4) << endl;
            /*cout << "min 8 = " << minSumRowValue << "; index = " << get_min_row_index(8) << endl;
            cout << "min 10 = " << minSumRowValue << "; index = " << get_min_row_index(10) << endl;
            cout << "min 16 = " << minSumRowValue << "; index = " << get_min_row_index(16) << endl;
            cout << "min 32 = " << minSumRowValue << "; index = " << get_min_row_index(32) << endl;*/
        }
    }

    double timeAfter = omp_get_wtime();

    cout << "Total time - " << timeAfter - timeBefore << " seconds" << endl;
    return 0;
}

void fill_my_array() {
    for (int i = 0; i < ROWS; i++) {
        for (int j = 0; j < COLUMNS; j++)
        {
            myArray[i][j] = i + j;
        }
    }
}

long long get_sum_array(int threads_count) {
    long long sum = 0;
    double timeBefore = omp_get_wtime();
#pragma omp parallel for reduction(+:sum) num_threads(threads_count)
    for (int i = 0; i < ROWS; i++) {
        for (int j = 0; j < COLUMNS; j++)
        {
            sum += myArray[i][j];
        }
    }
    double timeAfter = omp_get_wtime();

    cout << "Sum of " << threads_count << " threads worked - " << timeAfter - timeBefore << " seconds" << endl;

    return sum;
}

int get_min_row_index(int threads_count) {
    int index = -1;
    long long sum = 0;
    minSumRowValue = LLONG_MAX;
    double timeBefore = omp_get_wtime();
#pragma omp parallel for reduction(+:sum) num_threads(threads_count)
    for (int i = 0; i < ROWS; i++) {
        for (int j = 0; j < COLUMNS; j++)
        {
            sum += myArray[i][j];
        }
        if (sum < minSumRowValue) {
#pragma omp critical
            if (sum < minSumRowValue)
            {
                minSumRowValue = sum;
                index = i;
            }
        }
    }
    double timeAfter = omp_get_wtime();

    cout << "Min of " << threads_count << " threads worked - " << timeAfter - timeBefore << " seconds" << endl;

    return index;
}