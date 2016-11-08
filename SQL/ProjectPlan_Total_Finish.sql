select distinct PL.ProjectPlanId,PL.title
from ProjectPlan as PL
left join Project as P on PL.ProjectPlanId=P.ProjectPlanId
where  P.progress in('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128')