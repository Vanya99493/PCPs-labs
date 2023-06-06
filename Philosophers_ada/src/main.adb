with Ada.Text_IO; use Ada.Text_IO;
with GNAT.Semaphores; use GNAT.Semaphores;

procedure Main is
   philosophers_count : constant Integer := 5;
   laps_count : constant Integer := 5;
   thinking_time : constant Duration := 0.1;
   eating_time : constant Duration := 0.1;

   forks : array (1..philosophers_count) of Counting_Semaphore(1, Default_Ceiling);

   task type philosopher is
      entry start(id_in : Integer; left_fork_id_in : Integer; right_fork_id_in : Integer);
   end philosopher;

   task body philosopher is
      id : Integer;
      left_fork_id : Integer;
      right_fork_id : Integer;
   begin
      accept start(id_in : in Integer; left_fork_id_in : in Integer; right_fork_id_in : in Integer) do
         id := id_in;
         left_fork_id := left_fork_id_in;
         right_fork_id := right_fork_id_in;
      end start;

      for I in 1..laps_count loop
         Put_Line("Philosopher " & id'Img & " thinking " & I'Img & " times");
         delay thinking_time;

         forks(left_fork_id).Seize;
         Put_Line("Philosopher " & id'Img & " take left fork");
         forks(right_fork_id).Seize;
         Put_Line("Philosopher " & id'Img & " take right fork");

         Put_Line("Philosopher " & id'Img & " eating " & I'Img & " times");
         delay eating_time;

         forks(left_fork_id).Release;
         Put_Line("Philosopher " & id'Img & " drop left fork");
         forks(right_fork_id).Release;
         Put_Line("Philosopher " & id'Img & " drop right fork");
      end loop;
   end philosopher;

   philosophers : array(1..philosophers_count) of philosopher;
begin
   philosophers(1).start(1, 1, 2);
   for I in 2..philosophers_count loop
      philosophers(I).start(I, I rem philosophers_count + 1, I);
   end loop;
end Main;
