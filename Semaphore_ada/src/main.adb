with ada.Text_IO; use ada.Text_IO;
with ada.Strings.Fixed; use ada.Strings.Fixed;
with GNAT.Semaphores; use GNAT.Semaphores;
with Ada.Containers.Indefinite_Doubly_Linked_Lists; use Ada.Containers;

procedure Main is
   package String_Lists is new Indefinite_Doubly_Linked_Lists (String);
   use String_Lists;

   storage_size : constant Integer := 5;
   items_count : constant Integer := 41;
   producers_count : constant Integer := 2;
   customers_count : constant Integer := 5;

   pick_delay : constant Duration := 1.0;
   push_delay : constant Duration := 0.0;

   access_to_storage_sem : Counting_Semaphore (1, Default_Ceiling);
   push_item_sem : Counting_Semaphore (storage_size, Default_Ceiling);
   pick_item_sem : Counting_Semaphore (0, Default_Ceiling);

   items_counter : Integer := 1;
   storage : List := Empty_List;

   task type producer is
      entry start(items_count_in : Integer; push_delay_in : Duration);
   end producer;

   task type customer is
      entry start(items_count_in : Integer; pick_delay_in : Duration);
   end customer;

   task body producer is
      items_count : Integer;
      push_delay : Duration;
   begin
      accept start(items_count_in : in Integer; push_delay_in : Duration) do
         begin
            items_count := items_count_in;
            push_delay := push_delay_in;
         end;
      end start;

      for I in 1..items_count loop
         push_item_sem.Seize;
         delay push_delay;
         access_to_storage_sem.Seize;

         storage.Append("item " & items_counter'Img);
         Put_Line("Push " & "item " & items_counter'Img);
         items_counter := items_counter + 1;

         access_to_storage_sem.Release;
         pick_item_sem.Release;
      end loop;
   end producer;

   task body customer is
      items_count : Integer;
      pick_delay : Duration;
   begin
      accept start(items_count_in : Integer; pick_delay_in : Duration) do
         begin
            items_count := items_count_in;
            pick_delay := pick_delay_in;
         end;
      end start;

      for I in 1..items_count loop
         pick_item_sem.Seize;
         delay pick_delay;
         access_to_storage_sem.Seize;

         declare
            item : String := First_Element(storage);
         begin
            Put_Line("Pick " & item);
         end;

         storage.Delete_First;

         access_to_storage_sem.Release;
         push_item_sem.Release;
      end loop;
   end customer;

   producers : array (1..producers_count) of producer;
   customers : array (1..customers_count) of customer;

   items_of_one_producer : Integer := items_count / producers_count;
   items_of_one_customer : Integer := items_count / customers_count;
begin
   for I in 1..(producers_count - 1) loop
      producers(I).start(items_of_one_producer, push_delay);
   end loop;
   producers(producers_count).start(items_count - (items_of_one_producer * (producers_count - 1)), push_delay);

   for I in 1..(customers_count - 1) loop
      customers(I).start(items_of_one_customer, pick_delay);
   end loop;
   customers(customers_count).start(items_count - (items_of_one_customer * (customers_count - 1)), pick_delay);
end Main;
