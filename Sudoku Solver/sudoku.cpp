#include<iostream>
#include <stdlib.h>
#include <string.h>

using namespace std;

int sudoku[9][9];
int original[9][9];
int a=-1;		//current position
int constraints[10];

void echo();

void getConstraints(int x,int y)
{
	for(int k=0;k<10;k++)
		constraints[k]=0;
	//check row/column
	for(int i=0;i<9;i++)
	{
		if(x!=i)
			constraints[sudoku[i][y]]=1;
		if(y!=i)
			constraints[sudoku[x][i]]=1;
	}
	
	//check box
	int h=(x/3)*3;
	int k=(y/3)*3;
	
	for(int i=0;i<9;i++)
	{
		if(h+i%3!=x && k+i/3!=y)
		constraints[sudoku[h+i%3][k+i/3]]=1;
	}
	
}

int move()
{
	do
	{
		
		a--;
	}while(original[a%9][a/9]>0 && a>=0);
	if (a<0)
	{
		//return 0;			// overflow
		cout <<"overflow";
		exit(0);
	}
	return 1;
}


void solve()
{
	while(1)
	{
	
		a++;							//
		if(a>=81)						//
			break;						//move next
		if(original[a%9][a/9]>0)		//
			continue;					//
		
		getConstraints(a%9,a/9);
		int value=sudoku[a%9][a/9];
		
		do	//default value is zero if not then value is being corrected
		{
			value++;			// get the next value which is not constraint
		}while(constraints[value]);		
		
		if(value==10)	//means there is a mistake, go back and fix
		{
			
			sudoku[a%9][a/9]=0;	// since we're going back we have to null the value
								// of current cell
			
			
			move();
			move();	// to go back 1 step i have to process two back steps
							// because continue will cause automatic next step
							// due to the while loop
			
			continue;
		}
		else
		{
			sudoku[a%9][a/9]=value;
		}
	}
	
	
	
}


void echo_plain()
{
	for(int i=0;i<9;i++)
	{
		for(int j=0;j<9;j++)
		{
			cout << sudoku[j][i];
		}
	}
}

void echo()
{
	for(int i=0;i<9;i++)
	{
		cout << "\t";
		for(int j=0;j<9;j++)
		{
			cout << sudoku[j][i] << " ";
			if(j==2||j==5)
				cout << "\t";
		}
		if(i==2||i==5)
				cout << "\n";
		cout << "\n";
	}
}

int main(int argc, char* argv[])
{
	
	for (int i=0;i<81;i++)
	{
		sudoku[i%9][i/9]= argv[1][i]-48;
		original[i%9][i/9]=sudoku[i%9][i/9];
	}
	
	
	solve();

	echo_plain();

	return 0;
}
