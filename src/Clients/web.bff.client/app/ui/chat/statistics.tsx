import { fetchStatistics } from "@/app/lib/data";
import { lusitana } from '@/app/ui/fonts';
import { Suspense } from "react";
import { convertDateTimeString } from "@/app/lib/utils";

interface Statistic {
  count: number;
  type: string;
  created: string;
}

export default async function StatisticsChart({ inputTake, inputPage, inputType }: { inputTake: Number, inputPage: Number, inputType: string }) {
  const take = Number(inputTake) || 20;
  let page = Number(inputPage) || 1;
  page -= 1;

  const statistics: Statistic[] = await fetchStatistics(page, take, inputType);
  console.log(statistics);

  return (
    <div className="w-full md:col-span-4">
      <h2 className={`${lusitana.className} mb-4 text-xl md:text-2xl`}>
        Statistics
      </h2>
      <Suspense fallback={<div>Loading...</div>}>
          <div className="w-full md:col-span-4">
              {statistics.map((statistic: Statistic, i) => (
                      <div key={i} className="flex flex-col gap-2 text-sm">
                          <div className="w-full rounded-md bg-blue-300 p-1 mb-2">
                              {convertDateTimeString(statistic.created)}: {statistic.type} - {statistic.count}
                          </div>
                      </div>
                  ))}
          </div>
      </Suspense>
    </div>
  );
}
