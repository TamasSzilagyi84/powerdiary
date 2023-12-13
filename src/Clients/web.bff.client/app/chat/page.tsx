import ChatMessages from '@/app/ui/chat/chat-messages';
import Dropdown from '@/app/ui/chat/take-dropdown';
import Paging from '@/app/ui/chat/paging';

export default async function Page({
    searchParams,
  }: {
    searchParams?: {
      take?: string;
      page?: string;
    };
  }) {
    const take = Number(searchParams?.take) || 20;
    const page = Number(searchParams?.page) || 1;

    return (
        <div>
            <Dropdown value={take} />
            <Paging value={page} />
            <ChatMessages inputTake={take} inputPage={page} />
        </div>
        
    );
}
