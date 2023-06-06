with Ada.Text_IO;
use Ada.Text_IO;

procedure Main is
   --threads_counts : Integer := 4;
   --calculation_time: array (1 .. threads_counts) of Duration := (2.0, 1.5, 3.0, 2.5);
   --steps: array (1..threads_counts) of Long_Long_Integer := (1, 3, 2, 2);

   threads_counts : Integer := 2;
   calculation_time: array (1 .. threads_counts) of Duration := (2.0, 3.0);
   steps: array (1..threads_counts) of Long_Long_Integer := (1, 3);

   can_stop : array(1..threads_counts) of Boolean := (others => False);
   pragma Atomic (can_stop);


   task type breaker is
      entry Init(time_in : Duration; id_in : Integer);
   end breaker;

   task type calculator is
      entry Init(step_in : Long_Long_Integer; id_in : Integer; calculation_time_in : Duration);
   end calculator;

   task body breaker is
      time : Duration;
      id :Integer;
   begin
      accept Init(time_in : in Duration; id_in : in Integer) do
         begin
            time := time_in;
            id := id_in;
         end;
      end Init;

      delay time;
      can_stop(id) := True;
   end breaker;

   task body calculator is
      steps_count  : Long_Long_Integer := 0;
      sum  : Long_Long_Integer := 0;

      calculation_time : Duration;
      step : Long_Long_Integer;
      id : Integer;
   begin
      accept Init(step_in : in Long_Long_Integer; id_in : in Integer; calculation_time_in : in Duration) do
         begin
            step := step_in;
            id := id_in;
            calculation_time := calculation_time_in;
         end;
      end Init;
      loop
         steps_count := steps_count + 1;
         exit when can_stop(id);
      end loop;
      sum := steps_count * step;
      Put_Line ("Task Id: " & Id'Img & "; Sum: " & sum'Img & "; Steps count: " & steps_count'Img & "; Calculation time: " & calculation_time'Img);
   end calculator;

   calculators: array (1..threads_counts) of calculator;
   breakers: array (1..threads_counts) of breaker;

begin
   for I in calculators'Range loop
      calculators(I).Init(steps(I), i, calculation_time(i));
      breakers(I).Init(calculation_time(I), i);
   end loop;

end Main;
