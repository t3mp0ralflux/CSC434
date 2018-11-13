(* Brent Belanger *)
(* CSC 434 Assignment 5 *)
(* Problems 10, 11, 12, 15, 17, 21, 28 *)

(* 10:
	Write a function dupList of type 'a list -> 'a list whose output list is the same as the input list,
	but with each element of the input list repeated twice in a row.
*)

fun dupList l = foldr(fn (a, b) => a::a::b) [] l;


(* 11:
	Write a function mylength of type 'a list -> int that returns the length of a list.
*)

fun mylength l = foldr(fn (_, y) => 1 + y) 0 l;


(* 12:
	Write a function il2absrl of type int list -> real int that takes a list of integers and returns a list containing
	the absolute vaules of those integers, converted to real numbers.
*)

fun il2absrl l = foldr(fn (a,b) => real(abs(a))::b) [] l;


(* 15:
	Write a function myimplode that works just like the predefined implode.
	It should be a function of type char list -> string that takes a list of characters
	and returns the string containing those same characters in that same order.
*)

fun myimplode l = foldr (op ^) "" (foldr (fn (a,b) => String.str(a)::b) [] l);

(* 17.
	Write a function max of type int list -> int that returns the largest element
	Your function does not need to behave well if the list is empty.
*)

fun max l = foldr (fn (a, b) => if a > b then a else b) 0 l;

(* 21.
	Define a function less of type int * int list -> int list
    so that (e,L) is a list of all the integers in L that are less than e (in any order)
*)

fun less (e, L) = foldr (fn (a, b) => if a < e then a::b else b) [] L

(* 28.
	Define a function myfoldl with the same type and behavior as foldl.
*)
fun myfoldl f b [] = b  |  myfoldl f b (x::xs) =  myfoldl f (f (x,b)) xs;
