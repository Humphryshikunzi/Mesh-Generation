tri_0_1_ga = df_ga[df_ga<0.1] 

tri_0_2_ga = df_ga[df_ga>0.1] 
tri_0_2_ga = tri_0_2_ga[df_ga<0.2]

tri_0_3_ga = df_ga[df_ga>0.2] 
tri_0_3_ga = tri_0_3_ga[df_ga<0.3] 

tri_0_4_ga = df_ga[df_ga>0.3] 
tri_0_4_ga = tri_0_4_ga[df_ga<0.4] 

tri_0_5_ga = df_ga[df_ga>0.4] 
tri_0_5_ga = tri_0_5_ga[df_ga<0.5]

tri_0_6_ga = df_ga[df_ga>0.5] 
tri_0_6_ga = tri_0_6_ga[df_ga<0.6]


tri_0_7_ga = df_ga[df_ga>0.6] 
tri_0_7_ga = tri_0_7_ga[df_ga<0.7] 
 

tri_0_8_ga = df_ga[df_ga>0.7] 
tri_0_8_ga = tri_0_8_ga[df_ga<0.8] 


tri_0_9_ga = df_ga[df_ga>0.8] 
tri_0_9_ga = tri_0_9_ga[df_ga<0.9] 


tri_1_0_ga = df_ga[df_ga>0.9]  

print(1)
print(tri_0_1_ga.count())
print(2)
print(tri_0_2_ga.count())
print(3)
print(tri_0_3_ga.count())
print(4)
print(tri_0_4_ga.count())
print(5)
print(tri_0_5_ga.count())
print(6)
print(tri_0_6_ga.count())
print(7)
print(tri_0_7_ga.count())
print(8)
print(tri_0_8_ga.count())
print(9)
print(tri_0_9_ga.count())
print(10)
print(tri_1_0_ga.count()) 
 