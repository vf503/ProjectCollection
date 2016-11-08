select  COUNT(distinct PL.ProjectPlanId)
from ProjectPlan as PL
left join Project as P on PL.ProjectPlanId=P.ProjectPlanId
where P.CaptureReceiveDelayDate is not null or P.ProductionReceiveDelayDate is not null