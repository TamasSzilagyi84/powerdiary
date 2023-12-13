import Statistics from '@/app/ui/chat/statistics';
import TypeDropdown from '@/app/ui/chat/aggregation-type-dropdown';
import TakeDropdown from '@/app/ui/chat/take-dropdown';
import Paging from '@/app/ui/chat/paging';

export default async function Page({
    searchParams,
  }: {
    searchParams?: {
      take?: string;
      page?: string;
      type?: string;
    };
  }) {
    const take = Number(searchParams?.take) || 20;
    const page = Number(searchParams?.page) || 1;
    const type = searchParams?.type || 'Yearly';

    return (
        <div>
            <TypeDropdown value={type} />
            <TakeDropdown value={take} />
            <Paging value={page} />
            <Statistics inputTake={take} inputPage={page} inputType={type} />
        </div>
        
    );
}
