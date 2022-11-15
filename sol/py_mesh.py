import csv
import numpy as np  
import math 

L = 0.1
h = 0.08
r = 0.02

nb_y_element = 106
nb_x_element = 80
 
f = open('py_mapFile.csv', 'w', newline='') 
writer = csv.writer(f)

for y in np.linspace(0, h, num=nb_y_element):    
    row = []        
    for x in np.linspace(L, 0, num=nb_x_element):  
        if (x == 0 or x == L or y == 0 or y == h):
            row.append(1)   
            continue   
        if(y<r):
            if x<r:                 
                x0 = math.sqrt(math.pow(r,2) - math.pow(y,2))
                if(x<x0):
                     row.append(0)
                else:
                    row.append(1)   
            else:
                row.append(1) 
        else:
            row.append(1)             
    writer.writerow(row)    

f.close()            