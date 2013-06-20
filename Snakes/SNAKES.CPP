#include<stdio.h>
#include<graphics.h>
#include<conio.h>
#include<dos.h>
#include<time.h>
#include<stdlib.h>
#include<ctype.h>
#include<string.h>

//Game Define
int BODY;
int BACK;
int BORD;
int HEAD;
int FOOD;
int WALL;
int TEXT;

const int HEIGHT=95;
const int WIDTH=127;

const int STARTLENGTH=10;
const int STARTSPEED=30;
const int FULLSPEED=10;

//Game Over Constant
const char gameover[]={"001111100011100011000110111111100011111001100011011111110111111|0110000001101100111011101100000001100011011000110110000001100011|1100000011000110111111101100000001100011011000110110000001100011|1100111011000110111111101111110001100011011101110111111001100111|1100011011111110110101101100000001100011001111100110000001111100|011001101100011011000110110000000110001100011100011000000110111|0011111011000110110001101111111000111110000010000111111101100111|"};
const char snakes[]={"011111000000000000000000000111|110001100000000000000000000011|110001100000000000000000000011|011000000110111000011110000011001100011111000011111|0011100000110011000000110000110110001100011001100011|00001100001100110001111100001111000011111110001100|00000110001100110011001100001111000011000000000111|1100011000110011001100110000110110001100000000000011|1100011000110011001100110000110011001100011001100011|011111000011001100011101100111001100011111000011111"};

//Define a point
struct point
{
	int x;
	int y;
};

void theme(char theme)        //set colors according to theme
{
	if(theme==0)
	{
		BODY=YELLOW;
		BACK=BLUE;
		BORD=WHITE;
		HEAD=YELLOW;
		FOOD=BROWN;
		WALL=LIGHTRED;
		TEXT=WHITE;
	}
	else
	{
		BODY=BLUE;
		BACK=BLACK;
		BORD=WHITE;
		HEAD=RED;
		FOOD=GREEN;
		WALL=YELLOW;
		TEXT=LIGHTGRAY;
	}
}

void draw(int x,int y, int width, int height, int color,int text=0)
{
	setcolor(color);

	//set fill color
	setfillstyle(SOLID_FILL,color);

	if(text==0)
	{
		x=x*5+1;
		y=y*5+1;
	}

	rectangle(x,y,x+width-1,y+height-1);

	//fill rectangle
	floodfill(x+1,y+1,color);
}


void drawfood(int x,int y)
{
	//polygon point definition
	int foodShape[]={2,0,4,2,2,4,0,2,2,0};

	x=x*5+1;
	y=y*5+1;

	for(int i=1;i<=10;i++)
	{
		if(i%2==1)
			foodShape[i-1]+=x;
		else
			foodShape[i-1]+=y;
	}


	setcolor(FOOD);

	setfillstyle(SOLID_FILL,FOOD);

	fillpoly(5,foodShape);
}

void unpause(int grid[WIDTH][HEIGHT],int score)
{

	//remove pause text


	setcolor(BACK);


	//unprint score
	char scoreboard[80]={"PAUSED    SCORE: "};
	char myscore[50];

	itoa(score,myscore,10);
	strcat(scoreboard,myscore);

	//print cstring at x,y
	outtextxy(10,10,scoreboard);


	outtextxy(10,30,"Press R to reset.");
	outtextxy(10,40,"Press escape to quit.");

	draw(1,1,230,60,BACK);

	for(int i=0;i<46;i++)
	{
		for(int j=0;j<12;j++)
		{
			if(grid[i][j]==0)
				draw(i,j,5,5,BACK);
			if(grid[i][j]>0)
				draw(i,j,5,5,BODY);
			if(grid[i][j]==1)
				draw(i,j,5,5,HEAD);
			if(grid[i][j]==-1)
				drawfood(i,j);
			if(grid[i][j]==-2)
				draw(i,j,5,5,HEAD);
		}
	}
}

int maxwidth()
{
	//return ((getmaxx()/5)*5+1);
	return (WIDTH*5+1);
}

int maxheight()
{
	//return ((getmaxy()/5)*5+1);
	return (HEIGHT*5+1);
}

void setborder()        //Game border
{
	setcolor(BORD);
	rectangle(0,0,maxwidth(),maxheight());
	draw(1,1,maxwidth()-1,maxheight()-1,BACK,1);
}

void score(int score)       //Determine Score
{
	//score is actually the amount of food eaten by the snake

	char scoreboard[80]={"SCORE: "};
	char myscore[50];
	itoa(score,myscore,10);
	strcat(scoreboard,myscore);

	//print cstring at x,y
	outtextxy(10,10,scoreboard);
}

void printTitle(const char title[],int height,int x_offset,int y_offset,int boxsize,int color)
{
	int x=0,y=0;
	int i=0;

	for(y=0;y<height;y++)
	{
		while(title[i]!='|')
		{
			if(title[i]=='1')
			{
				//determine the size and position of each pixel of the word from the constant
				draw(x*boxsize + x_offset,y*boxsize + y_offset,boxsize,boxsize,color,1);
			}
			x++;
			i++;
		}
		x=0;
		i++;
	}
}

void lost(int myscore)
{
	//score is actually the amount of food eaten by the snake



	//print game over from the "game over" constant
	printTitle(gameover,7,62,210,8,TEXT);

	score(myscore);

	outtextxy(424,10,"SNAKES BY KARAN JIT SINGH");
	outtextxy(10,maxheight()-30 + 4,"Press R to reset.");
	outtextxy(10,maxheight()-20 + 4,"Press any other key to quit.");

	//Wait for 2 seconds
	delay(2000);
}

int main()
{
	clrscr();
	int initializing = 1;

	//Familiarize user with controls


	printf("\n\n\tControls:\n\n");
	printf("\t\t UP         :  W \n");
	printf("\t\t DOWN       :  S \n");
	printf("\t\t LEFT       :  A \n");
	printf("\t\t RIGHT      :  D \n");
	printf("\t\t MAX SPEED  :  SPACE \n");
	printf("\t\t PAUSE      :  P \n");
	printf("\t\t QUIT       :  ESC \n\n");
	printf("\tPress any key to Start...");
	getch();

	clrscr();

	theme(1);

	char *bgiPath = "BGI";                  //bgi path

	//startgame(bgiPath);            //initialize game

	reset:          //Restart Game


	int length=STARTLENGTH;     //Default length from "game constants"


	int x=63-length,y=46;       //Initial position of snake

	int grid[WIDTH][HEIGHT]={0};
	int i,j;

	int speed=STARTSPEED;       //Default speed from "game constants"


	int maxspeed=0;             /*fullspeed variable
									0 denotes fullspeed
									1 denotes normal speed      */

	int score=0;              //score is the amount of food eaten

	int cheat[2]={0,0};         /*	0 denotes cheat off
									1 denotes cheat on

									cheat[0] is supersonic
									cheat[1] is fluffy
								 */

	//Initialize initgraph variables
	int gd=DETECT ,gm;      //DETECT is already defined in graphics.h


	initgraph(&gd,&gm,bgiPath);       //initialize graphics

	if(initializing ==1)
	{
		initializing=0;
		printTitle(snakes,10,116,180,8,TEXT); 		//print snakes title
		
		if (getch()=='s')	//hidden theme
			theme(0);

		printTitle(snakes,10,116,180,8,BLACK);  	//repaint snakes title
	}
	//Define game area border
	setborder();

	//draw walls
	for(i=0;i<WIDTH;i++)
		for(j=0;j<HEIGHT;j++)
			if(grid[i][j]==-2)
				draw(i,j,5,5,WALL);


	//initialize/draw snake
	for(i=0;i<length;i++)
	{
		draw(x,y,5,5,i+1==length?HEAD:BODY);

		/*
			Every part of the body is represented as the rank of the body part

			for eg: the value of head in the grid[][] array will always be equal to 1
					and value of last part of the body will be equal to the length of
					the snake
		*/

		grid[x][y]=length-i;

		x+=1;
	}
	x-=1;   //bring x to the position of the head of the snake because in the above loop it was x+=1 was inetiated in the end


	//move snake direction
	int keys;

	int direction=3;    /*
							Set of Directions:
							UP      = 0
							LEFT    = 1
							DOWN    = 2
							RIGHT   = 3
						 */

	//randomize
	srand(unsigned(time(NULL)));

	//position of food
	point food;
	food.x=-1;
	food.y=-1;

	while(1)
	{

		//Randomize food
		if(food.x==-1)          //if there is no food available
		{
			do
			{
				//radomize position of food
				food.x= rand()%WIDTH;
				food.y= rand()%HEIGHT;

			} while(grid[food.x][food.y]!=0);       //loop untill the randomized position is empty so that we can place food in it

			grid[food.x][food.y]=-1;                    //place food
			drawfood(food.x,food.y);        //draw food
		}


		if(kbhit()!=0)      //if a key on the keyboard of hit
		{
			keys=getch();       //get the key which was hit

            if(keys==27)        //if key is escape then end game
                break;              //break; initiated here will end the while loop

            switch(keys)
            {
                case 'w':
                case 'W':
                    if(direction!=2)        //if current direction is not down
                        direction=0;
                    break;
                    
                case 'a':
                case 'A':
                    if(direction!=3)        //if current direction is not right
                        direction=1;
                    break;
		
                case 's':
                case 'S':
                    if(direction!=0)        //if current direction is not up
                        direction=2;
                    break;
                    
                case 'd':
                case 'D':
                    if(direction!=1)        //if current direction is not right
                        direction=3;
                    break;

                case 32:
                    if(maxspeed==0)         //if maxspeed is not enabled
                        maxspeed=1;             //enable maxspeed
                    else
                        maxspeed=0;             //disable maxspeed
                    break;

                case 'p':                       //if key pressed is 'p' :: pause the game
                case 'P':
                    
                    //Checking for key hit while game is paused 
                    
                    int k=0;
                    
                    char cheatcode[3][14];
                    
                    strcpy(cheatcode[0],"SUPERSONIC");
                    strcpy(cheatcode[1],"FLUFFY");
                    strcpy(cheatcode[2],"THECAKEISALIE");
                    
                    setcolor(TEXT);
                    
                    //print score while paused
                    char scoreboard[80]={"PAUSED    SCORE: "};
                    char myscore[50];
                    
                    itoa(score,myscore,10);
                    strcat(scoreboard,myscore);
                    
                    //print cstring at x,y
                    outtextxy(10,10,scoreboard);
                    
                    outtextxy(10,30,"Press R to reset.");
                    outtextxy(10,40,"Press escape to quit.");
                    
                    
                    while(1)
                    {
                        
                        if(kbhit!=0)
                        {
                            int key=getch();
                            switch(key)
                            {
                                case 'p':       //if 'p' is pressed :: unpause the game
                                case 'P':
                                          
                                    if(k==0)    //if user is not typing the cheatcode
                                    {
                                        unpause(grid,score);    //repaint that part of grid
                                
                                        goto endpause;
                                    }
                                    else
                                    {
                                        goto trycheat;
                                    }
						
                                case 27:        //if 'escape' is pressed :: quit the game
                                    
                                    unpause(grid,score);    //repaint that part of grid
                                    
                                    goto lose;
        
                                case 'r':       //if 'r' is pressed :: reset the game
                                case 'R':
                                    if(k==0)    //if user is not typing the cheatcode
                                    {
                                        closegraph();
                                        goto reset;
                                    }
                                    else
                                    {
                                        goto trycheat;
                                    }
                                default:
                                trycheat:
                                    recheat:
                                    
                                    if(k==0)
                                    {
                                        for (i=0; i<=1; i++) {
                                            if(toupper(key)==cheatcode[i][0])
                                            {
                                                k++;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                    
                                        if(toupper(key)==cheatcode[i][k])          //if key pressed is the corresponding key of the cheat code
                                            k++;
                                        else
                                        {
                                            k=0;
                                            goto recheat;
                                        }
                                        if(k==strlen(cheatcode[i]))                //if the cheat has been typed correctly
                                        {
                                            //toggle cheat
                                            k=0;
                                            if(cheat[i]==1)
                                            {
                                                cheat[i]=0;
                                            }
                                            else
                                            {
                                                cheat[i]=1;
                                            }
                                        }
                                    }
                            }
                        }
                    }
                    endpause:
                    break;
            }
        }
        
        /*
            the variable speed is not defined as a value which is greater
            it is just the delay between each movement of code which
            determines the movement of the snake.
         
            Therefore the lesser the value of the variable 'speed' more
            will be the speed of the snake, because the delay in the code
            execution will be less
        */
        if (cheat[0]==0) {     //if supersonic cheat is enabled
            delay(maxspeed==1?FULLSPEED:speed);     //if maxspeed is enabled then fullspeed else normal speed
        }
        else
        {
            //delay(1);               //JUMBOJET
        }
            
		

        /*
            If the snake touches the tunnel that is the border of the game
            area it must come out of the opposite side.
        */
        
		switch(direction)
		{   
            case 0:
				if(y!=0 && (grid[x][y-1]<=0 || grid[x][y-1]==length) )             //if the snake is not entering the tunnel and the next position is anything but a body part of the snake
                    y-=1;   //Move Up
                    
				else if(y==0 && (grid[x][HEIGHT-1]<=0 || grid[x][HEIGHT-1]==length))        /*if the snake is entering the tunnel and the next position is anything but a body part of the
                                                                                             snake but the next part is allowed to be the tail as it will move forward and we wont lose*/
                    y=HEIGHT-1;   //Come out of the bottom side
				
                else        //If there is a body part on the next position :: game over
                    goto lose;
                
                break;
                
			case 1:
				if(x!=0 && (grid[x-1][y]<=0 || grid[x-1][y]==length))                 //if the snake is not entering the tunnel and the next position is anything but a body part of the snake
                    x-=1;   //Move Left
                
				else if(x==0 && (grid[WIDTH-1][y]<=0 || grid[WIDTH-1][y]==length))        /*if the snake is entering the tunnel and the next position is anything but a body part of the snake
                                                                                           but the next part is allowed to be the tail as it will move forward and we wont lose*/
                    x=WIDTH-1;
                
				else        //If there is a body part on the next position :: game over
                    goto lose;
                break;
			
            case 2:
				if(y!=HEIGHT-1 && (grid[x][y+1]<=0 || grid[x][y+1]==length))            //if the snake is not entering the tunnel and the next position is anything but a body part of the snake
                    y+=1;   //Move Down
                
				else if(y==HEIGHT-1 && (grid[x][0]<=0 || grid[x][0]==length))         /*if the snake is entering the tunnel and the next position is anything but a body part of the snake but
                                                                                       the next part is allowed to be the tail as it will move forward and we wont lose*/
                    y=0;
                
				else        //If there is a body part on the next position :: game over
                    goto lose;
                break;
			case 3:
				if(x!=WIDTH-1 && (grid[x+1][y]<=0 || grid[x+1][y]==length))           //if the snake is not entering the tunnel and the next position is anything but a body part of the snake
                    x+=1;   //Move Right
                
				else if(x==WIDTH-1 && (grid[0][y]<=0 || grid[0][y]==length))        /*if the snake is entering the tunnel and the next position is anything but a body part of the snake but
                                                                                     the next part is allowed to be the tail as it will move forward and we wont lose*/
                    x=0;
                
				else        //If there is a body part on the next position :: game over
                    goto lose;
                break;
		}

		//current x,y are the poisition of the head
        
        if(grid[x][y]==-2)
		{
			goto lose;
		}
        
        
		draw(x,y,5,5,HEAD);     //Draw the head at its position, adding 1 because the border is of 1 pixel

		for(i=0;i<WIDTH;i++)              //Search horizontal pixels
        {
            for(j=0;j<HEIGHT;j++)           //Search vertical pixels
            {
                if(grid[i][j]==length)                  //if grid[i][j] is the last part of the body of the snake, the parts of the body are represented as the rank of the part 
                {
                    if(grid[x][y]!=-1 && cheat[1]==0)                  //if the next piece is not the food i.e. -1         and     fluffy cheat is not enabled 
                    {
                        
                            grid[i][j]=0;           //the part becomes blank because the last part moves forward
                            if(i!=x || j!=y)
                            {
                                draw(i,j,5,5,BACK);     //draw background
                            }
                    }
                    else                    //if the next piece is food
                    {
                        length++;                       //if the piece was food the length of the snake is increased by one
                        
                        if(grid[x][y]==-1)
                        {
                            score++;
                            food.x=-1;                      //set food as -1 => food has been eaten
                        }
                        
                        if(speed>FULLSPEED)             //if the current code delay is more than mindelay/maxspeed
                        {
                            speed=STARTSPEED-(((score)/5)*2);              //decrease code delay after eating of every 5 food pieces
                        }
                        grid[i][j]=length;              //keep the last part and put its rank
                    }
                }
                else if(grid[i][j]>0 && grid[i][j]!=length)         //if grid[i][j] is a body part and it is not the last part
                {
                    grid[i][j]++;               //increase its rank because the snake is moving forward
                    if(grid[i][j]==2)           //if grid[i][j] was the head and now is increased by 1 becomes 2 so we have to color it as a normal body part
                    {
                        draw(i,j,5,5,BODY);
                    }
                }
            }
        }
		grid[x][y]=1;       //set the rank of head on thhe current position
	}
    
	lose:           //when the game is over
        lost(score);
	
    //if any extra key is pressed
    if(kbhit()!=0)
        getch();
    
    int b=getch();      //get last command
	
    closegraph();       //close graph
    
	if(b=='r'||b=='R') goto reset;      //key was 'r' :: reset game
	return 0;
}