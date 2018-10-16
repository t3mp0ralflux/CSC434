use "hw4.sml";

fun pass_fail x = if x then "Pass" else "Fail";

fun test(function, input, expected_output) = pass_fail (function(input) = expected_output);


print("*********TEST RESULTS******************\n");
print("\n1.Pow: "^test(pow, (2,3), 8)^"\n");
print("\n2.Pow: "^test(pow, (~2,3), ~8)^"\n");
print("\n3.Pow: "^test(pow, (2,0), 1)^"\n");
print("\n4.Binary: "^test(binary, 17, "010001")^"\n");
print("\n5.Binary: "^test(binary, 1023, "01111111111")^"\n");
print("\n6.Binary: "^test(binary, 117, "01110101")^"\n");
print("\n7.Repeat: "^test(repeat, ("hello", 3), "hellohellohello")^"\n");
print("\n8.CountNegative: "^test(countNegative,[3,17,~9,34,~7,2],2)^"\n");
print("\n9.CountNegative: "^test(countNegative,[3,17,9,34,7,2],0)^"\n");
print("\n10.absList: "^test(absList,[(~38,47), (983,~14), (~17,~92), (0,34)], [(38,47),(983,14), (17,92), (0,34)])^"\n");
print("\n11.split: "^test(split,[5,6,8,17,93,0],[(2,3), (3,3), (4,4), (8,9), (46,47), (0,0)])^"\n");
print("\n12.split: "^test(split,[5,1,8,17,93,0],[(2,3), (0,1), (4,4), (8,9), (46,47), (0,0)])^"\n");
print("\n13.isSorted: "^test(isSorted, [1,2,3,4,4,5,5], true)^"\n");
print("\n14.isSorted: "^test(isSorted, [1,2,3,4,3,5,5], false)^"\n");
print("\n15.isSorted: "^test(isSorted, [1], true)^"\n");
print("\n16.isSorted: "^test(isSorted, [], true)^"\n");
print("\n17.isSorted: "^test(isSorted, [1,1,1,1,1], true)^"\n");
print("\n18.Collapse: "^test(collapse,[1,3,5,19,7,4],[4,24,11])^"\n");
print("\n19.Collapse: "^test(collapse,[1,2,3,4,5],[3,7,5])^"\n");
print("\n20.Collapse: "^test(collapse,[1,2],[3])^"\n");
print("\n21.Collapse: "^test(collapse,[1],[1])^"\n");
print("\n22.Insert: "^test(insert,(8,[1,3,7,9,22,38]), [1,3,7,8,9,22,38])^"\n");
print("\n23.Insert: "^test(insert,(88,[1,3,7,9,22,38]), [1,3,7,9,22,38,88])^"\n");
print("\n24.Insert: "^test(insert,(0,[1,3,7,9,22,38]), [0,1,3,7,9,22,38])^"\n");
print("\n25.Insert: "^test(insert,(22,[1,3,7,9,22,38]), [1,3,7,9,22,22,38])^"\n");

