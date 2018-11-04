use "hw5.sml";

(* Sort a list of integers. *)
fun myMergeSort nil = nil
| myMergeSort [e] = [e]
| myMergeSort theList =
     let
         (* From the given list make a pair of lists
         * (x,y), where half the elements of the
         * original are in x and half are in y. *)
         fun halve nil = (nil, nil)
         | halve [a] = ([a], nil)
         | halve (a::b::cs) =
             let
             val (x, y) = halve cs
             in
             (a::x, b::y)
             end; 
         (* Merge two sorted lists of integers into
         * a single sorted list. *)
         fun merge (nil, ys) = ys
         | merge (xs, nil) = xs
         | merge (x::xs, y::ys) =
         if (x < y) then x :: merge(xs, y::ys)
         else y :: merge(x::xs, ys);
         val (x, y) = halve theList
         in
         merge(myMergeSort x, myMergeSort y)
     end;
     
fun pass_fail x = if x then "Pass" else "Fail";

fun test(function, input, expected_output) = pass_fail (function(input) = expected_output);

fun test_set(function, input, expected_output) = pass_fail (myMergeSort(function(input)) = expected_output);

print("*********TEST RESULTS******************\n");
print("\n1.Quicksort "^test(quicksort, [1,3,2,5,4,8,7,9,0], [0,1,2,3,4,5,7,8,9])^"\n");
print("\n2.Quicksort "^test(quicksort, [1,3,2,5,4,2, 8,6,7,9,0], [0,1,2,2,3,4,5,6,7,8,9])^"\n");
print("\n3.Member "^test(member, (2,[1,3,2,5,4,2, 8,6,7,9,0]), true)^"\n");
print("\n4.Member "^test(member, (12,[1,3,2,5,4,2, 8,6,7,9,0]), false)^"\n");
print("\n5.Union "^test_set(union, ([1,3,2,5], [0,1,2,33]),[0,1,2,3,5,33])^"\n");
print("\n6.Union "^test_set(union, ([1,3,2,5], [5,2,3,1]),[1,2,3,5])^"\n");
print("\n7.Union "^test_set(union, ([1,3,2,5], []),[1,2,3,5])^"\n");
print("\n8.intersection "^test_set(intersection, ([1,3,22,5], [5,12,3,1]),[1,3,5])^"\n");
print("\n9.intersection "^test_set(intersection, ([1,3,2,5], [15,12,13,11]),[])^"\n");
print("\n10.intersection "^test_set(intersection, ([1,3,2,5], [5,2,3,1]),[1,2,3,5])^"\n");
print("\n11.Range "^test(range, (2,12,3), [2,5,8,11])^"\n");
print("\n12.Range "^test(range, (12,34,5), [12,17,22,27,32])^"\n");
print("\n13.Range "^test(range, (10,2,~1), [10,9,8,7,6,5,4,3])^"\n");
print("\n14.Slice "^test(slice, (range(2,20,2),2,7), [6,8,10,12,14])^"\n");
print("\n15.Slice "^test(slice, (range(1,100,4),3,12), [13, 17, 21, 25, 29, 33, 37, 41, 45])^"\n");

