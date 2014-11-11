#include<iostream>
#include<conio.h>

using namespace std;

int sudoku[9][9];


int check(int a, int n)
{
	int h=a/9,k=a%9;
	
	for(int i=0;i<9;i++)
	{
		if(sudoku[h][i]==n || sudoku[i][k]==n)
			return 0;
		if(sudoku[(h/3)*3 + i/3][(k/3)*3 + i%3]==n)
			return 0;
	}
	
	return 1;
}

int solve()
{
	
	for(int a=0;a<81;a++)
	{
		if(sudoku[a/9][a%9]==0)
		{
			for(int i=1;i<=9;i++)
			{
				if(check(a,i))
				{
					sudoku[a/9][a%9]=i;
					if(!solve())
						sudoku[a/9][a%9]=0;
					else
						return 1;
				}
			}
			sudoku[a/9][a%9]=0;
			return 0;
		}
	}
	return 1;
}

void print()
{	
	cout << "\n";
	for(int i=0;i<9;i++)
	{
		if(i%3==0)
			cout << "\n";

		for(int j=0;j<9;j++)
		{
			if(j%3==0)
				cout << "\t";
			cout << sudoku[i][j] << " ";
		}
		cout << "\n";
	}
}

void print_plain()
{
	for(int i=0;i<81;i++)
	{
		cout << sudoku[i/9][i%9];
	}
}

int main(int argc, char* argv[])
{
	for(int i=0;i<81;i++)
	{
		//cin >> sudoku[i/9][i%9];
		//sudoku[i/9][i%9]=0;
		sudoku[i/9][i%9] = argv[1][i]-48;
	}
		
	if(!solve())
		cout << "overflow";
	else
		print_plain();
	
	return 0;
}
