import csv
import numpy as np  
import math 

L = 100
h = 80

# 6
# nb_y_element = 106 
# nb_x_element = 80

# 5
nb_y_element = 128
nb_x_element = 96

# circle
r = 6
c_1 = [20, 20]

# triangle
p_1 = [30, 30]
p_2 = [30, 60]
p_3 = [70, 70]

###################################################    triangle
# A utility function to calculate area
# of triangle formed by (x1, y1),
# (x2, y2) and (x3, y3)
 
def t_area(x1, y1, x2, y2, x3, y3):

    return abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0)
 
# A function to check whether point P(x, y)
# lies inside the triangle formed by
# A(x1, y1), B(x2, y2) and C(x3, y3)
def isInside_t(x1, y1, x2, y2, x3, y3, x, y):
 
    # Calculate area of triangle ABC
    A = t_area (x1, y1, x2, y2, x3, y3)
 
    # Calculate area of triangle PBC
    A1 = t_area (x, y, x2, y2, x3, y3)
     
    # Calculate area of triangle PAC
    A2 = t_area (x1, y1, x, y, x3, y3)
     
    # Calculate area of triangle PAB
    A3 = t_area (x1, y1, x2, y2, x, y)
     
    # Check if sum of A1, A2 and A3
    # is same as A
    if(A == A1 + A2 + A3):
        return 1
    else:
        return 0 
 

##################################################      Circle

def isInside_c(circle_x, circle_y, rad, x, y):
     
    # Compare radius of circle
    # with distance of its center
    # from given point
    if ((x - circle_x) * (x - circle_x) +
        (y - circle_y) * (y - circle_y) <= math.pow(rad, 2)):
        return True
    else:
        return False  
 
 # csv file for writing
f = open('py_mapFileNoSense.csv', 'w', newline='') 
writer = csv.writer(f)


# for all cols
for y in np.linspace(0, h, num=nb_y_element):    
    row = []        

    # for all rows
    for x in np.linspace(L, 0, num=nb_x_element):  

        # draw solid line, boundary
        if (x == 0 or x == L or y == 0 or y == h):
            row.append(1)   
            continue   

        # draw triangle
        if(isInside_t(p_1[0],p_1[1],p_2[0],p_2[1],p_3[0],p_3[1], x,y)):
            row.append(0) 

        # draw circle
        elif(isInside_c(c_1[0],c_1[1], r, x, y)):
            row.append(0)

        # fill the rest with ones, the outer figure
        else:
            row.append(1)  

    # write the row to csv           
    writer.writerow(row)    

# close the file after saving all rows
f.close()            