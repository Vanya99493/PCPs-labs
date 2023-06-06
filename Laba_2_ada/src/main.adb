with Ada.Text_IO; use Ada.Text_IO;
with Ada.Integer_Text_IO; use Ada.Integer_Text_IO;
with Ada.Numerics.Discrete_Random;

procedure Main is
   threads_count : constant Integer := 8;
   array_length : constant Integer := 10000;
   min_value : constant Integer := 0;
   max_value : constant Integer := 100;

   step : constant Integer := array_length / threads_count;

   minimum_element : Integer;
   minimum_element_index : Integer;
   arr : array (1..array_length) of Integer;

   procedure fill_array is
      type RandInRange is new Integer range min_value..max_value;
      package rand_integer is new Ada.Numerics.Discrete_Random(RandInRange);
      use rand_integer;
      gen : Generator;
      num : RandInRange;
   begin
      reset(gen);
      for I in arr'Range loop
         num := Random(gen);
         arr(I) := Integer(num);
      end loop;
   end fill_array;

   procedure change_random_el is
      type RandInteger is new Integer range 1..array_length;
      package rand_integer is new Ada.Numerics.Discrete_Random(RandInteger);
      use rand_integer;
      gen : Generator;
      num : RandInteger;
   begin
      reset(gen);
      num := Random(gen);
      --Put_Line(Integer(num)'Img & " index of min el");
      arr(Integer(num)) := Integer'First;
   end change_random_el;

   protected minimum_locker is
      procedure compare_minimum(minimum : Integer; minimum_index : Integer);
      entry export_minimum;
   private
      minimum_el : Integer := Integer'Last;
      minimum_el_index : Integer;
      threads_ended : Integer := 0;
   end minimum_locker;

   task type find_local_minimum is
      entry start(lower_index : Integer; upper_index : Integer);
   end find_local_minimum;

   protected body minimum_locker is
      procedure compare_minimum(minimum : Integer; minimum_index : Integer) is
      begin
         if(minimum < minimum_el) then
            minimum_el := minimum;
            minimum_el_index := minimum_index;
         end if;
         threads_ended := threads_ended + 1;
      end compare_minimum;

      entry export_minimum when threads_ended = threads_count is
      begin
         minimum_element := minimum_el;
         minimum_element_index := minimum_el_index;
      end export_minimum;
   end minimum_locker;

   task body find_local_minimum is
      local_minimum_element : Integer := Integer'Last;
      local_minimum_element_index : Integer := -1;
      --lower_index, upper_index : Integer;
   begin
      accept start(lower_index : Integer; upper_index : Integer) do
         for I in lower_index..upper_index loop
            if(arr(I) < local_minimum_element) then
               local_minimum_element := arr(I);
               local_minimum_element_index := I;
            end if;
            --Put_Line(I'Img);
         end loop;
      minimum_locker.compare_minimum(local_minimum_element, local_minimum_element_index);
      end start;
   end find_local_minimum;

   threads_arr : array (1..threads_count) of find_local_minimum;
begin
   fill_array;
   change_random_el;

   --for I in arr'Range loop
   --   Put_Line(arr(I)'Img);
   --end loop;

   for I in threads_arr'Range loop
      threads_arr(I).start((I - 1) * step + 1, step * I);
   end loop;

   minimum_locker.export_minimum;

   Put_Line("Minimum element: " & minimum_element'Img & "; Index: " & minimum_element_index'Img);

end Main;
