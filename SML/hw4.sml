(* Brent Belanger *)
(* CSC434-1 *)
(* 10/13/2018 *)

(* #1 - pow *)
(* Input: a = integer, b = integer*)
(* Output: a to the power of b as integer *)
(* Notes: Does as advertised on the tin.  Returns the power of an integer. *)
fun pow (a, b) = if b = 0 then 1 else a*pow(a,b-1);


(* #2 - sumTo *)
(* Input: x = integer*)
(* Output: The sum of 1 + 1/2 + 1/3 + ... + 1/x *)
fun sumTo x = if x = 0 then 0.0 else 1.0/real(x) + sumTo(x-1);
               
               
(* #3 - repeat *)
(* Input: aStr = string, n = integer*)
(* Output: aStr repeated n times eg: "Hello" = "HelloHelloHello" *)
fun repeat (aStr, n) = if n = 0 then "" else aStr ^ repeat(aStr, n-1);

(* #4 - binary *)
(* Input: x = integer*)
(* Output: string of x as binary *)
(* Notes: This function requires two helpers.  The first takes the integer and turns it into binary
   The second takes that binary and turns each number into a char 
   Finally, binary puts all of that together into one string*)
fun intToBin x = if x > 0 then intToBin(x div 2)@[(x mod 2)] else [];
fun binToChar nil = nil | binToChar (x::xs) = if x = 1 then #"1"::binToChar(xs) else #"0"::binToChar(xs);
fun binary x = if x = 0 then "" else implode (#"0"::binToChar(intToBin(x)));

(* #5 - countNegative *)
(* Input: x = list of integers*)
(* Output: number of negatives in the list *)
(* Notes: The helper for this simply returns a 1 if the number is negative, 0 if positive
   countNegative goes through each item in the list and returns the count of all negatives *)
fun isNegative x = if x < 0 then 1 else 0;
fun countNegative x = if null x then 0 else isNegative(hd x) + countNegative(tl x);


(* #6 - absList *)
(* Input: x = list of int * int tuples *)
(* Output: absolute value of list of int * int tuples *)
(* Notes:  This is a rough one.  The helper is easy, it just returns the absolute value of the integer given
   absList takes the list, separates the head, takes the absolute value of the tuple, and concats it with the tail until the list is empty *)
fun abs x = if x < 0 then ~x else x;
fun absList (x : (int * int) list) = if null x then nil else (abs(#1(hd (x))), abs(#2(hd (x))))::absList(tl x);


(* #7 - split *)
(* Input: x = list of integers *)
(* Output: (int * int) List *)
(* Notes: the helper takes each integer and divides it, returning a tuple of two values.  If odd, puts the remainder on the second value of the tuple.
   split iterates through a list of integers and returns a list of tuples *)
fun intToTuple x = if x mod 2 = 1 then (x div 2, (x div 2) + 1) else (x div 2, x div 2);
fun split x = if null x then nil else intToTuple(hd x)::split(tl x);


(* #8 - isSorted *)
(* Input: x = list of integers *)
(* Output: returns true if list is sorted, false if not sorted *)
(* Notes: listLength returns the length of the list.  If the list given to isSorted only has one value, well...
   isSorted compares the head of the list to the first value of the tail.  If the head value is higher, then false, else, continue to the end of the list. *)
fun listlength x = if null x then 0 else 1 + listlength(tl x);
fun isSorted x = if null x then true else if listlength(x)=1 then true else if hd x > hd(tl x) then false else if hd x <= hd(tl x) then (true; isSorted(tl x)) else false;

 
(* #9 - collapse *)
(* Input: x = list of integers *)
(* Output: collapsed list of integers where pairs are summed eg: (1,2 *) 
(* Notes: This one has to use type matching: '|'.  The first returns x if passed x::nil.  The other is more fun.
   This takes x, y, and zt where zt is the 3rd item and beyond part of the input.  
   If zt (the remainder) is greater than two items, then add x and y and concat with the rest of zt, continuing to chug along down the zt path. 
   If the list is exactly two, then add x+y and concat with the last item in the list (summed, of course).
   If the last item is exactly one, then just concat x+y with zt.
   For all other cases, just add x+y and call it a day. *)
fun collapse (x::nil) = [x] | collapse (x::y::zt) = if listlength(zt) > 2 then (x+y)::collapse(zt) else if listlength(zt) = 2 then (x+y)::[((hd zt)+(hd(tl zt)))] else if listlength(zt) = 1 then (x+y)::zt else [x+y];

        
(* #10 - insert *)
(* Input: a = integer, b = list of sorted ascending integers*)
(* Outut: inserts a into the sorted list in the proper spot *)        
fun insert(a, nil) = [a] | insert(a,b::bs) = if a<b then a::b::bs else b::insert(a,bs);
